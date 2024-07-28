using SimpleRPG.Infrastructure.Factory;
using UnityEngine;

namespace SimpleRPG.Enemy
{
    public class LootSpawner : MonoBehaviour
    {

        [SerializeField] private EnemyDeath _enemyDeath;
        private IGameFactory _factory;
        private int _lootMin;
        private int _lootMax;


        public void Construct(IGameFactory factory)
        {
            _factory = factory;
        }
        private void Start()
        {
            _enemyDeath.Happened += SpawnLooot;
        }

        private void SpawnLooot()
        {
           LootPiece lootPiece = _factory.CreateLoot();
           lootPiece.transform.position = transform.position;

           var lootItem = new Loot();
           int value = Random.Range(_lootMin, _lootMax);
           lootItem.SetValue(value);
           lootPiece.Initialize(lootItem);
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}