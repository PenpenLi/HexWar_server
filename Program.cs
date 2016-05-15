using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using HexWar;

namespace HexWar_server
{
    class Program
    {
        private static void WriteLog(string _str)
        {
            Console.WriteLine(_str);
        }

        static void Main(string[] args)
        {
            Log.Init(WriteLog);

            ConfigDictionary.Instance.LoadLocalConfig("local.xml");

            StaticData.path = ConfigDictionary.Instance.table_path;

            StaticData.Load<MapSDS>("map");

            Map.Init();

            StaticData.Load<HeroTypeSDS>("heroType");

            StaticData.Load<HeroSDS>("hero");

            Dictionary<int, HeroSDS> heroDic = StaticData.GetDic<HeroSDS>();

            Dictionary<int, IHeroSDS> newHeroDic = new Dictionary<int, IHeroSDS>();

            foreach(KeyValuePair<int,HeroSDS> pair in heroDic)
            {
                newHeroDic.Add(pair.Key, pair.Value);
            }

            StaticData.Load<SkillSDS>("skill");

            Dictionary<int, SkillSDS> skillDic = StaticData.GetDic<SkillSDS>();

            Dictionary<int, ISkillSDS> newSkillDic = new Dictionary<int, ISkillSDS>();

            foreach(KeyValuePair<int,SkillSDS> pair in skillDic)
            {
                newSkillDic.Add(pair.Key, pair.Value);
            }

            Battle.Init(newHeroDic, Map.mapDataDic);

            Battle2.Init(newHeroDic, Map.mapDataDic, newSkillDic);

            Server<PlayerUnit> server = new Server<PlayerUnit>();

            server.Start("0.0.0.0", 1983, 100);

            while (true)
            {
                server.Update();



                Thread.Sleep(10);
            }
        }
    }
}
