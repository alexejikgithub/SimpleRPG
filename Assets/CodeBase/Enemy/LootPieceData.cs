using System;
using UnityEngine;

namespace SimpleRPG.Enemy
{
    [Serializable]
    public class LootPieceData
    {
        [SerializeField] [HideInInspector] private string _spawnerId;
        [SerializeField] [HideInInspector] private Vector3 _position;

        public LootPieceData(string spawnerId, Vector3 position)
        {
            _spawnerId = spawnerId;
            _position = position;
        }
            
        public string SpawnerId => _spawnerId;
        public Vector3 Position => _position;
    }
}