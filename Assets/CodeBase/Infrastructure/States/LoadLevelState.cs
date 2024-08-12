using System.Threading.Tasks;
using SimpleRPG.Logic;
using SimpleRPG.CameraLogic;
using SimpleRPG.Infrastructure.Factory;
using UnityEngine;
using SimpleRPG.Hero;
using SimpleRPG.Services.PersistantProgress;
using SimpleRPG.StaticData;
using SimpleRPG.UI.Elements;
using SimpleRPG.UI.Services.Factory;
using UnityEngine.SceneManagement;

namespace SimpleRPG.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
		private readonly IPersistantProgressService _progressService;
		private readonly IStaticDataService _staticData;
		private readonly IUiFactory _uiFactory;

		private const string EnemySpawnerTag = "EnemySpawner";

		public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IPersistantProgressService progressService, IStaticDataService staticData, IUiFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
			_progressService = progressService;
			_staticData = staticData;
			_uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.Cleanup();
            _gameFactory.WarmUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private async void OnLoaded()
        {
	        await InitUiRoot();
			await InitGameWorld();
            InformProgressReaders();

			_stateMachine.Enter<GameLoopState>();
		}

        private async Task InitGameWorld()
        {
	        var levelData = GetLevelStaticData();

	        await InitSpawners(levelData);
	        GameObject hero = await _gameFactory.CreateHero(levelData);
	        StartCameraFollow(hero);
	        await InitHud(hero);
        }

        private async Task InitUiRoot()
        {
	        await _uiFactory.CreateUIRoot();
        }

        private async Task InitHud(GameObject hero)
        {
	        GameObject hud = await _gameFactory.CreateHud();
	        hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
        }

        private void InformProgressReaders()
		{
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.PlayerProgress);
            }
		}

        private LevelStaticData GetLevelStaticData()
		{
			string sceneKey = SceneManager.GetActiveScene().name;
			LevelStaticData levelData = _staticData.ForLevel(sceneKey);
			return levelData;
		}

        private async Task InitSpawners(LevelStaticData levelData)
		{
			
			foreach (var enemySpawnerData in levelData.EnemySpawners)
			{
			 await	_gameFactory.CreateSpawner(enemySpawnerData.Position, enemySpawnerData.Id,
					enemySpawnerData.EnemyTypeId);
			}
		}

        private void StartCameraFollow(GameObject hero)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }
    }
}