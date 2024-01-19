using SimpleRPG.Infrastructure.AssetManagement;
using SimpleRPG.Infrastructure.Factory;
using SimpleRPG.Infrastructure.Services;
using SimpleRPG.Services.Input;
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
            _sceneLoader.Load(Initial,onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>("Main");
        }

        private void RegisterServices()
        {
            _allServices.RegisterSingle<IInputService>(InputService());
            _allServices.RegisterSingle<IAssetProvider>(new AssetProvider());
            _allServices.RegisterSingle<IGameFactory>(new GameFactory(_allServices.Single<IAssetProvider>()));
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