using System;
using System.Collections.Generic;
using CodeBase.Logic;
using SimpleRPG.Infrastructure.Factory;
using SimpleRPG.Infrastructure.Services;

namespace SimpleRPG.Infrastructure.States
{
    public class GameStateMachine 
    {
        private readonly Dictionary<Type, IExitableState> _state;
        private IExitableState _activeState;
        
        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, AllServices services)
        {
            _state = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this,sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this,sceneLoader,curtain, services.Single<IGameFactory>()),
                [typeof(GameLoopState)] = new GameLoopState(this)
            };
        }
        
        public void Enter<TState>() where TState  :class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

       


        public void Enter<TState,TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }
        
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }
        private TState GetState<TState>() where TState : class, IExitableState => _state[typeof(TState)] as TState;
    }
}
