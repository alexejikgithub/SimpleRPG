using SimpleRPG.Enemy;
using SimpleRPG.Infrastructure.Services;
using SimpleRPG.Logic;
using UnityEngine;

namespace SimpleRPG.Infrastructure.Factory
{
    public interface ILootSpawner : IService
    {
       LootPiece SpawnLoot(Vector3 position, EnemyTypeId enemyType);
    }
}