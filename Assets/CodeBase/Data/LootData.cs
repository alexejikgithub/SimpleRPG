using System;
using SimpleRPG.Enemy;
using UnityEngine;

namespace SimpleRPG.Data
{
    [Serializable]
    public class LootData
    {
        public Action Changed;

        [SerializeField] private int _collectedAmount;

        public int CollectedAmount => _collectedAmount;


        public void Collect(Loot loot)
        {
            _collectedAmount += loot.Value;
            Changed?.Invoke();
        }
        public void Add(int loot)
        {
            _collectedAmount += loot;
            Changed?.Invoke();
        }
    }
}