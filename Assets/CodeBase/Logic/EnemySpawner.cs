using SimpleRPG.Data;
using SimpleRPG.Enemy;
using SimpleRPG.Infrastructure.Factory;
using SimpleRPG.Infrastructure.Services;
using SimpleRPG.Services.PersistantProgress;
using UnityEngine;

namespace SimpleRPG.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress, ISavedProgressReader
    {
        [SerializeField] private EnemyTypeId enemyType;
        [SerializeField] private bool _isDead;

        private string _id;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
            {
                _isDead = true;
            }
            else
            {
                Spawn();
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_isDead)
            {
                progress.KillData.ClearedSpawners.Add((_id));
            }
        }

        private void Spawn()
        {
            GameObject enemy = _factory.CreateEnemy(enemyType, transform);
            _enemyDeath = enemy.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += Died;
        }

        private void Died()
        {
            if (_enemyDeath != null)
            {
                _enemyDeath.Happened -= Died;
            }

            _isDead = true;
        }
    }
}