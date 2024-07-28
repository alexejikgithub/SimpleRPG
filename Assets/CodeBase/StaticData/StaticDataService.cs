using System.Collections.Generic;
using System.Linq;
using SimpleRPG.Logic;
using UnityEngine;

namespace SimpleRPG.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;

        public void LoadEnemies()
        {
            _enemies = Resources.LoadAll<EnemyStaticData>("StaticData/Enemies")
                .ToDictionary(x => x.EnemyTypeId, x => x);
        }

        public EnemyStaticData ForEnemy(EnemyTypeId typeId)
        {
            if (_enemies.TryGetValue(typeId, out EnemyStaticData staticData))
            {
                return staticData;
            }

            return null;
        }
    }
}