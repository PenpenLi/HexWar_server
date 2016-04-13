using System;
using System.IO;

public class PlayerUnit : IUnit
{
    private Action<MemoryStream> sendDataCallBack;

    public void Init(Action<MemoryStream> _sendDataCallBack)
    {
        sendDataCallBack = _sendDataCallBack;

        BattleManager.Instance.PlayerEnter(this);
    }

    public void ReceiveData(byte[] _bytes)
    {
        BattleManager.Instance.ReceiveData(this, _bytes);
    }

    public void SendData(MemoryStream _ms)
    {
        sendDataCallBack(_ms);
    }
}

