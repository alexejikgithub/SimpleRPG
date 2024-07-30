using System.Collections.Generic;
using System.Linq;
using SimpleRPG.Logic;
using UnityEngine;

namespace SimpleRPG.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;
        private Dictionary<string, LevelStaticData> _levels;

        public void LoadEnemies()
        {
            _enemies = Resources.LoadAll<EnemyStaticData>("StaticData/Enemies")
                .ToDictionary(x => x.EnemyTypeId, x => x);
            _levels = Resources.LoadAll<LevelStaticData>("StaticData/Levels")
                .ToDictionary(x => x.LevelKey, x => x);
        }

        public EnemyStaticData ForEnemy(EnemyTypeId typeId)
        {
            if (_enemies.TryGetValue(typeId, out EnemyStaticData staticData))
            {
                return staticData;
            }

            return null;
        }

        public LevelStaticData ForLevel(string sceneKey)
        {
            if (_levels.TryGetValue(sceneKey, out LevelStaticData staticData))
            {
                return staticData;
            }

            return null;
        }
    }
}