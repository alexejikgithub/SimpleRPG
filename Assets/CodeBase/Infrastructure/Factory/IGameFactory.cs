using SimpleRPG.Infrastructure.Services;
using SimpleRPG.Services.PersistantProgress;
using System.Collections.Generic;
using SimpleRPG.Enemy;
using SimpleRPG.Logic;
using UnityEngine;

namespace SimpleRPG.Infrastructure.Factory
{
    public interface IGameFactory: IService
    {
	    List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }

		GameObject CreateHero(GameObject initialPoint);
        GameObject CreateHud();
		void Cleanup();
		
		GameObject CreateEnemy(EnemyTypeId enemyType, Transform parent, ILootSpawner lootSpawner);
		LootPiece CreateLoot();
		void CreateSpawner(Vector3 position, string SpawnerId, EnemyTypeId enemyTypeId);
    }
}