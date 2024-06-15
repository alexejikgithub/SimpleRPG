using SimpleRPG.Logic;
using SimpleRPG.CameraLogic;
using SimpleRPG.Infrastructure.Factory;
using UnityEngine;
using System;
using SimpleRPG.Services.PersistantProgress;

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

		public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IPersistantProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
			_progressService = progressService;
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
			InitGameWorld();
            InformProgressReaders();

			_stateMachine.Enter<GameLoopState>();
		}

		private void InformProgressReaders()
		{
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.PlayerProgress);
            }
		}

		private void InitGameWorld()
		{
			var hero = _gameFactory.CreateHero(GameObject.FindWithTag(InitialPointTag));
			StartCameraFollow(hero);
			_gameFactory.CreateHud();
		}

		private void StartCameraFollow(GameObject hero)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }
    }
}