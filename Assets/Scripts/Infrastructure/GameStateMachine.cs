using System;
using System.Collections.Generic;

namespace SimpleRPG.Infrastructure
{
    public class GameStateMachine 
    {
        private readonly Dictionary<Type, IState> _state;
        private IState _activeState;
        
        public GameStateMachine(SceneLoader sceneLoader)
        {
            _state = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this,sceneLoader),
                [typeof(LoadLevelState)] = new LoadLevelState(this,sceneLoader)
            };
        }
        
        public void Enter<TState>() where TState : IState
        {
            _activeState?.Exit();
            
            IState state = _state[typeof(TState)];
            _activeState = state;
            state.Enter();
        }
    }
}
