using SimpleRPG.Infrastructure.Factory;
using SimpleRPG.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace SimpleRPG.Enemy
{
    public class AgentToPlayerMovement : Follow
    {
        [SerializeField] private NavMeshAgent _agent;

        private Transform _heroTransform;

        private IGameFactory _gameFactory;

        private const float MinimalDistance = 2;

        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }
        
        private void Update()
        {
            MoveToHero();
        }

        private void MoveToHero()
        {
            if (IsHeroAbsent() || IsHeroReached())
                return;
            _agent.destination = _heroTransform.position;
        }

        private bool IsHeroAbsent()
        {
            return _heroTransform == null;
        }

        private bool IsHeroReached()
        {
            return Vector3.Distance(_heroTransform.position, _agent.transform.position) < MinimalDistance;
        }
    }
}