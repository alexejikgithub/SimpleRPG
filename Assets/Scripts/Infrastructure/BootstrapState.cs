using SimpleRPG.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimpleRPG.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(Initial,onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>("Main");
        }

        private void RegisterServices()
        {
            Game.InputService = RegisterInputService();
        }

        public void Exit()
        { 
        }
        
        private InputService RegisterInputService()
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