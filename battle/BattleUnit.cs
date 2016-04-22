using HexWar;
using System.IO;
using System.Collections.Generic;

public class BattleUnit
{
    private PlayerUnit mPlayer;
    private PlayerUnit oPlayer;

    private Battle2 battle;

    public void Init(PlayerUnit _mPlayer,PlayerUnit _oPlayer,List<int> _mCards,List<int> _oCards,int _mapID)
    {
        mPlayer = _mPlayer;
        oPlayer = _oPlayer;

        battle = new Battle2();

        battle.ServerSetCallBack(SendData);

        battle.ServerStart(_mapID, _mCards, _oCards);
    }
    
    public void ReceiveData(PlayerUnit _playerUnit,byte[] _bytes)
    {
        battle.ServerGetPackage(_bytes, _playerUnit == mPlayer);
    }

    private void SendData(bool _isMine,MemoryStream _ms)
    {
        if (_isMine)
        {
            mPlayer.SendData(_ms);
        }
        else
        {
            oPlayer.SendData(_ms);
        }
    }
}
