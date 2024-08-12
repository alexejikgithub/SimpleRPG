using SimpleRPG.Infrastructure.AssetManagement;
using SimpleRPG.Services.PersistantProgress;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleRPG.Enemy;
using SimpleRPG.Infrastructure.Services;
using SimpleRPG.Logic;
using SimpleRPG.Logic.EnemySpawners;
using SimpleRPG.StaticData;
using SimpleRPG.UI.Elements;
using SimpleRPG.UI.Services.Windows;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace SimpleRPG.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IPersistantProgressService _persistantProgressService;
        private readonly IWindowService _windowService;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameObject HeroGameObject { get; set; }

        public GameFactory(IAssetProvider assets, IStaticDataService staticData,
            IPersistantProgressService persistantProgressService, IWindowService windowService)
        {
            _assets = assets;
            _staticData = staticData;
            _persistantProgressService = persistantProgressService;
            _windowService = windowService;
        }

        public async Task WarmUp()
        {
            await _assets.Load<GameObject>(AssetsAddresses.Loot);
            await _assets.Load<GameObject>(AssetsAddresses.Spawner);
        }

        public async Task<GameObject> CreateHero(LevelStaticData levelStaticData)
        {
            HeroGameObject = await InstantiateRegistredAsync(AssetsAddresses.HeroPath, levelStaticData.InitialHeroPosition);
            return HeroGameObject;
        }

        public async Task<GameObject> CreateHud()
        {
            GameObject hud = await InstantiateRegistredAsync(AssetsAddresses.UIPath);
            hud.GetComponentInChildren<LootCounter>().Construct(_persistantProgressService.PlayerProgress.WorldData);

            foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
            {
                openWindowButton.Construct(_windowService);
            }

            return hud;
        }

        public async Task<GameObject> CreateEnemy(EnemyTypeId enemyType, Transform parent, ILootSpawner lootSpawner)
        {
            EnemyStaticData enemyData = _staticData.ForEnemy(enemyType);


            GameObject prefab = await _assets.Load<GameObject>(enemyData.PrefabRference);

            GameObject enemy = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);

            var health = enemy.GetComponent<EnemyHealth>();
            health.Current = enemyData.Hp;
            health.Max = enemyData.Hp;

            enemy.GetComponent<ActorUI>().Construct(health);

            enemy.GetComponent<AgentToPlayerMovement>()?.Construct(HeroGameObject.transform);
            enemy.GetComponent<RotateToHero>()?.Construct(HeroGameObject.transform);

            enemy.GetComponent<NavMeshAgent>().speed = enemyData.MoveSpeed;

            var enemyAttack = enemy.GetComponent<EnemyAttack>();
            enemyAttack.SetDamage(enemyData.Damage);
            enemyAttack.Construct(HeroGameObject.transform);
            return enemy;
        }

        public async Task<LootPiece> CreateLoot()
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetsAddresses.Loot);
            LootPiece lootPiece = InstantiateRegistred(prefab).GetComponent<LootPiece>();
            lootPiece.Construct(_persistantProgressService.PlayerProgress.WorldData);
            return lootPiece;
        }

        public async Task CreateSpawner(Vector3 position, string SpawnerId, EnemyTypeId enemyTypeId)
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetsAddresses.Spawner);
            
            SpawnPoint spawner = InstantiateRegistred(prefab, position).GetComponent<SpawnPoint>();
            spawner.Construct(SpawnerId, enemyTypeId, this, AllServices.Container.Single<ILootSpawner>());
        }

        private async Task<GameObject> InstantiateRegistredAsync(string prefabPath, Vector3 position)
        {
            GameObject gameObject = await _assets.Instantiate(prefabPath, position);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        private async Task<GameObject> InstantiateRegistredAsync(string prefabPath)
        {
            GameObject gameObject = await _assets.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        
        private GameObject InstantiateRegistred(GameObject prefab, Vector3 position)
        {
            GameObject gameObject = Object.Instantiate(prefab, position,Quaternion.identity);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        private GameObject InstantiateRegistred(GameObject prefab)
        {
            GameObject gameObject = Object.Instantiate(prefab);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
            _assets.Cleanup();
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }

            ProgressReaders.Add(progressReader);
        }
    }
}