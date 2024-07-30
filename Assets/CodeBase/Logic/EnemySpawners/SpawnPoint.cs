using System.Linq;
using SimpleRPG.Data;
using SimpleRPG.Enemy;
using SimpleRPG.Infrastructure.Factory;
using SimpleRPG.Infrastructure.Services;
using SimpleRPG.Services.PersistantProgress;
using UnityEngine;

namespace SimpleRPG.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress, ISavedProgressReader
    {
        private EnemyTypeId _enemyType;
        private bool _isDead;

        private string _id;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;
        private ILootSpawner _lootSpawner;
        private Vector3 _deathPosition;
        private LootPiece _lootPiece;
        private bool _isLootPickedUp;

        public void Construct(string id, EnemyTypeId enemyTypeId, IGameFactory factory, ILootSpawner lootSpawner)
        {
            _id = id;
            _enemyType = enemyTypeId;
            _factory = factory;
            _lootSpawner = lootSpawner;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
            {
                _isDead = true;
                var lootPieceData = progress.KillData.NonPickedLoot.FirstOrDefault(loot=>loot.SpawnerId==_id);
                if (lootPieceData!=null)
                {
                    progress.KillData.NonPickedLoot.Remove(lootPieceData);
                    SpawnLoot(lootPieceData.Position);
                }
                else
                {
                    _isLootPickedUp = true;
                    
                }
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
                progress.KillData.ClearedSpawners.Add(_id);
                var lootPieceData = progress.KillData.NonPickedLoot.FirstOrDefault(loot=>loot.SpawnerId==_id);
                if (!_isLootPickedUp)
                {
                    if (lootPieceData == null)
                    {
                        progress.KillData.NonPickedLoot.Add(new LootPieceData(_id, _deathPosition));
                    }
                }
                else
                {
                    if (lootPieceData != null)
                    
                    {
                        progress.KillData.NonPickedLoot.Remove(lootPieceData);
                    }
                }

            }
        }

        private void Spawn()
        {
            GameObject enemy = _factory.CreateEnemy(_enemyType, transform,_lootSpawner);
            _enemyDeath = enemy.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += Died;
        }

        private void SpawnLoot(Vector3 position)
        {
            _deathPosition = position;
            _lootPiece = _lootSpawner.SpawnLoot(_deathPosition, _enemyType);
            _lootPiece.PickedUp += PickupLoot;
        }

        private void PickupLoot()
        {
            _lootPiece.PickedUp -= PickupLoot;

            _isLootPickedUp = true;
            Debug.Log(_isLootPickedUp);
        }

        private void Died(Vector3 position)
        {
            if (_enemyDeath != null)
            {
                _enemyDeath.Happened -= Died;
            }

            SpawnLoot(position);
            
            _isDead = true;
        }
    }
}