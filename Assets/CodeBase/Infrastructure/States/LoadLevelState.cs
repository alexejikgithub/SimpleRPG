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
        private const string InitialPointTag = "InitialPoint";
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
            _sceneLoader.Load(sceneName, OnLoaded);
        }
        
        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
	        InitUiRoot();
			InitGameWorld();
            InformProgressReaders();

			_stateMachine.Enter<GameLoopState>();
		}

        private void InitUiRoot()
        {
	        _uiFactory.CreateUIRoot();
        }

        private void InformProgressReaders()
		{
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.PlayerProgress);
            }
		}

		private void InitHud(GameObject hero)
		{
			GameObject hud = _gameFactory.CreateHud();
			hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
		}

		private void InitGameWorld()
		{
			InitSpawners();
			var hero = _gameFactory.CreateHero(GameObject.FindWithTag(InitialPointTag));
			StartCameraFollow(hero);
			InitHud(hero);
		}
		
		private void InitSpawners()
		{
			string sceneKey = SceneManager.GetActiveScene().name;
			LevelStaticData levelData = _staticData.ForLevel(sceneKey);
			foreach (var enemySpawnerData in levelData.EnemySpawners)
			{
				_gameFactory.CreateSpawner(enemySpawnerData.Position, enemySpawnerData.Id,
					enemySpawnerData.EnemyTypeId);
			}
		}

		private void StartCameraFollow(GameObject hero)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }
    }
}