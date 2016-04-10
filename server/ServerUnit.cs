using System;
using System.Net.Sockets;
using System.IO;

public class ServerUnit
{
    private const int HEAD_LENGTH = 2;

    private Socket socket;

    private ushort bodyLength;

    private byte[] headBuffer = new byte[HEAD_LENGTH];

    private byte[] bodyBuffer = new byte[ushort.MaxValue];

    private IUnit unit;

    private bool isReceiveHead = true;

    internal void Init(Socket _socket, IUnit _unit)
    {
        socket = _socket;

        unit = _unit;
    }

    internal void Update()
    {
        if (isReceiveHead)
        {
            ReceiveHead();
        }
        else
        {
            ReceiveBody();
        }
    }

    private void ReceiveHead()
    {
        if (socket.Available >= HEAD_LENGTH)
        {
            socket.Receive(headBuffer, HEAD_LENGTH, SocketFlags.None);

            isReceiveHead = false;

            bodyLength = BitConverter.ToUInt16(headBuffer, 0);

            ReceiveBody();
        }
    }

    private void ReceiveBody()
    {
        if (socket.Available >= bodyLength)
        {
            socket.Receive(bodyBuffer, bodyLength, SocketFlags.None);

            isReceiveHead = true;
           
            byte[] resultBytes = new byte[bodyLength];

            Array.Copy(bodyBuffer, 0, resultBytes, 0, bodyLength);

            unit.ReceiveData(resultBytes);

            ReceiveHead();
        }
    }

    public void SendData(MemoryStream _ms)
    {
        byte[] head = BitConverter.GetBytes((ushort)_ms.Length);

        socket.BeginSend(head, 0, HEAD_LENGTH, SocketFlags.None, SendCallBack, null);
        
        socket.BeginSend(_ms.GetBuffer(), 0, (int)_ms.Length, SocketFlags.None, SendCallBack, null);
    }
    
    private void SendCallBack(IAsyncResult result)
    {

    }
}

