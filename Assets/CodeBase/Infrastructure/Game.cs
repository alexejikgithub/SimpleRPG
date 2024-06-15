using SimpleRPG.Logic;
using SimpleRPG.Infrastructure.Services;
using SimpleRPG.Infrastructure.States;

namespace SimpleRPG.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner),curtain, AllServices.Container);
        }
  
    }
}