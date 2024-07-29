using SimpleRPG.Infrastructure.Factory;
using SimpleRPG.Logic;
using SimpleRPG.StaticData;
using UnityEngine;

namespace SimpleRPG.Enemy
{
    public class LootSpawner : ILootSpawner
    {
        private IGameFactory _factory;
        private IStaticDataService _staticData;

        public LootSpawner(IGameFactory factory, IStaticDataService staticData)
        {
            _factory = factory;
            _staticData = staticData;
        }
        
        public LootPiece SpawnLoot(Vector3 position, EnemyTypeId enemyType)
        {
           LootPiece lootPiece = _factory.CreateLoot();
           lootPiece.transform.position = position;

           var lootItem = new Loot();
           EnemyStaticData enemyData = _staticData.ForEnemy(enemyType);
           int value = Random.Range(enemyData.MinLoot, enemyData.MaxLoot);
           lootItem.SetValue(value);
           lootPiece.Initialize(lootItem);
           return lootPiece;
        }
        
    }
}