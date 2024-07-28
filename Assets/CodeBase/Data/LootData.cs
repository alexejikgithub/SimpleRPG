using System;
using SimpleRPG.Enemy;

namespace SimpleRPG.Data
{
    [Serializable]
    public class LootData
    {
        public Action Changed;

        private int _collectedAmount;

        public int CollectedAmount => _collectedAmount;

        public void Collect(Loot loot)
        {
            _collectedAmount += loot.Value;
            Changed?.Invoke();
        }
    }
}