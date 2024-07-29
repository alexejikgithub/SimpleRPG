using System.ComponentModel;
using SimpleRPG.Enemy;
using SimpleRPG.Infrastructure.AssetManagement;
using SimpleRPG.Infrastructure.Factory;
using SimpleRPG.Infrastructure.Services;
using SimpleRPG.Infrastructure.Services.SaveLoad;
using SimpleRPG.Services.Input;
using SimpleRPG.Services.PersistantProgress;
using SimpleRPG.StaticData;
using UnityEngine;

namespace SimpleRPG.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _allServices;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _allServices = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
            RegisterStaticData();
            _allServices.RegisterSingle<IInputService>(InputService());
            _allServices.RegisterSingle<IAssetProvider>(new AssetProvider());
            _allServices.RegisterSingle<IPersistantProgressService>(new PersistantProgressService());
            _allServices.RegisterSingle<IGameFactory>(new GameFactory(_allServices.Single<IAssetProvider>(),
                _allServices.Single<IStaticDataService>(), AllServices.Container.Single<IPersistantProgressService>()));
            _allServices.RegisterSingle<ISaveLoadService>(
                new SaveLoadService(_allServices.Single<IPersistantProgressService>(),
                    _allServices.Single<IGameFactory>()));
            _allServices.RegisterSingle<ILootSpawner>(new LootSpawner(_allServices.Single<IGameFactory>(),
                _allServices.Single<IStaticDataService>()));
        }

        private void RegisterStaticData()
        {
            var staticData = new StaticDataService();
            staticData.LoadEnemies();
            _allServices.RegisterSingle<IStaticDataService>(staticData);
        }

        public void Exit()
        {
        }

        private InputService InputService()
        {
            if (Application.isEditor)
            {
                return new StandaloneInputService();
            }
            else
            {
                return new MobileInputService();
            }
        }
    }
}