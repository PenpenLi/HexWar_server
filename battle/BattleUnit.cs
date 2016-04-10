using HexWar;
using System.IO;
using System.Collections.Generic;

public class BattleUnit
{
    private PlayerUnit mPlayer;
    private PlayerUnit oPlayer;

    private Battle battle;

    public void Init(PlayerUnit _mPlayer,PlayerUnit _oPlayer,List<int> _mCards,List<int> _oCards,int _mapID)
    {
        mPlayer = _mPlayer;
        oPlayer = _oPlayer;

        battle = new Battle();

        battle.ServerStart(_mapID, _mCards, _oCards);

        RefreshData();
    }

    private void RefreshData()
    {
        using (MemoryStream ms = new MemoryStream())
        {
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                battle.ServerRefreshData(bw, true);

                mPlayer.SendData(ms);
            }
        }

        using (MemoryStream ms = new MemoryStream())
        {
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                battle.ServerRefreshData(bw, false);

                oPlayer.SendData(ms);
            }
        }
    }

    public void DoAction(PlayerUnit _playerUnit,byte[] _bytes)
    {
        bool result;

        using (MemoryStream ms = new MemoryStream(_bytes))
        {
            using (BinaryReader br = new BinaryReader(ms))
            {
                result = battle.DoAction(mPlayer == _playerUnit, br);
            }
        }

        if (result)
        {
            RefreshData();
        }
    }
}
