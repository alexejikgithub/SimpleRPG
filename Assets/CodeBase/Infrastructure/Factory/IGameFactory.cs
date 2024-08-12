using SimpleRPG.Infrastructure.Services;
using SimpleRPG.Services.PersistantProgress;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleRPG.Enemy;
using SimpleRPG.Logic;
using SimpleRPG.StaticData;
using UnityEngine;

namespace SimpleRPG.Infrastructure.Factory
{
    public interface IGameFactory: IService
    {
	    List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }

		Task<GameObject> CreateHero(LevelStaticData initialPoint);
        Task<GameObject> CreateHud();
		void Cleanup();
		
		Task<GameObject> CreateEnemy(EnemyTypeId enemyType, Transform parent, ILootSpawner lootSpawner);
		Task<LootPiece> CreateLoot();
		Task CreateSpawner(Vector3 position, string SpawnerId, EnemyTypeId enemyTypeId);
		Task WarmUp();
    }
}