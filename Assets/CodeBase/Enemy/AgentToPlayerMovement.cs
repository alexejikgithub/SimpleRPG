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

		private void Start()
		{
			_gameFactory = AllServices.Container.Single<IGameFactory>();
			if (_gameFactory.HeroGameObject != null)
			{
				InitializeHeroTransform();
			}
			else
			{
				_gameFactory.HeroCreated += OnHeroCreated;
			}
		}

		private void OnHeroCreated()
		{
			InitializeHeroTransform();
		}

		private void Update()
		{
			MoveToHero();
		}

		public void InitializeHeroTransform()
		{
			_heroTransform = _gameFactory.HeroGameObject.transform;
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