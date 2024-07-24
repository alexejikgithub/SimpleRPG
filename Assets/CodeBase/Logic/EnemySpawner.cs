using System;
using SimpleRPG.Data;
using SimpleRPG.Services.PersistantProgress;
using UnityEngine;

namespace SimpleRPG.Logic
{
    public class EnemySpawner : MonoBehaviour,ISavedProgress,ISavedProgressReader
    {
        [SerializeField] private MonsterTypeId _monster;
        [SerializeField] private bool _isDead;
        
        private string _id;
        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
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
            
        }
    }
}