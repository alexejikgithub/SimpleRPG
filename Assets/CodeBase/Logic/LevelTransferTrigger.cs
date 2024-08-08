using SimpleRPG.Infrastructure.Services;
using SimpleRPG.Infrastructure.States;
using UnityEngine;

namespace SimpleRPG.Logic
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        [SerializeField] private string _nextLevel = "Dungeon";
        private IGameStateMachine _statemachine;
        private const string _playerTag= "Player";

        private bool _triggered;

        private void Awake()
        {
            _statemachine = AllServices.Container.Single<IGameStateMachine>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_triggered)
                return;
            
            if (other.CompareTag(_playerTag))
            {
                _statemachine.Enter<LoadLevelState,string>(_nextLevel);
                _triggered = true;
            }
        }
    }
}