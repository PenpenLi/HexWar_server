using System.Collections.Generic;

public class BattleManager
{
    private static BattleManager _Instance;

    public static BattleManager Instance
    {
        get
        {
            if(_Instance == null)
            {
                _Instance = new BattleManager();
            }

            return _Instance;
        }
    }

    private Dictionary<PlayerUnit, BattleUnit> battleListWithPlayer = new Dictionary<PlayerUnit, BattleUnit>();

    private PlayerUnit lastPlayer = null;
    
    public void PlayerEnter(PlayerUnit _playerUnit)
    {
        if(lastPlayer == null)
        {
            lastPlayer = _playerUnit;
        }
        else
        {
            BattleUnit unit = new BattleUnit();

            List<int> mCards = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

            List<int> oCards = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

            unit.Init(lastPlayer, _playerUnit, mCards, oCards, 1);

            battleListWithPlayer.Add(lastPlayer, unit);
            battleListWithPlayer.Add(_playerUnit, unit);

            lastPlayer = null;
        }
    }

    public void PlayerDoAction(PlayerUnit _playerUnit,byte[] _bytes)
    {
        battleListWithPlayer[_playerUnit].DoAction(_playerUnit, _bytes);
    }
}

