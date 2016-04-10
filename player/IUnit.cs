using System;
using System.IO;

public interface IUnit
{
    void ReceiveData(byte[] _bytes);
    void Init(Action<MemoryStream> _sendDataCallBack);
    void SendData(MemoryStream _ms);
}
