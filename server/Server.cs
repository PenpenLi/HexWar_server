using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;


public class Server<T> where T : IUnit, new()
{
    private Socket socket;

    private List<ServerUnit> list = new List<ServerUnit>();

    public void Start(string _path, int _port, int _maxConnections)
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        socket.Bind(new IPEndPoint(IPAddress.Parse(_path), _port));

        socket.Listen(_maxConnections);

        BeginAccept();
    }

    private void BeginAccept()
    {
        socket.BeginAccept(SocketAccept, null);
    }

    private void SocketAccept(IAsyncResult _result)
    {
        Socket clientSocket = socket.EndAccept(_result);

        Console.WriteLine("One user connected");

        T unit = new T();

        ServerUnit serverUnit = new ServerUnit();

        lock (list)
        {
            list.Add(serverUnit);
        }

        serverUnit.Init(clientSocket, unit);

        unit.Init(serverUnit.SendData);

        BeginAccept();
    }

    internal void Update()
    {
        lock (list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Update();
            }
        }
    }
}

