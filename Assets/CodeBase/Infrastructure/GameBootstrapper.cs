using SimpleRPG.Logic;
using SimpleRPG.Infrastructure.States;
using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleRPG.Infrastructure
{
	public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _curtainPrefab;
        
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(_curtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}