using UnityEngine;
using UnityEngine.AI;

namespace SimpleRPG.Enemy
{
	[RequireComponent(typeof(NavMesh))]
	[RequireComponent(typeof(EnemyAnimator))]
	public class AnimationAlongAgent : MonoBehaviour
	{
		private const float MinimalVelocity = 0.1f;

		[SerializeField] private NavMeshAgent _agent;
		[SerializeField] private EnemyAnimator _animator;

		private void Update()
		{
			if(ShouldMove())
			{
				_animator.Move(_agent.velocity.magnitude);
			}
			else
			{
				_animator.StopMoving();
			}
		}

		private bool ShouldMove()
		{
			return _agent.velocity.magnitude > MinimalVelocity && _agent.remainingDistance > _agent.radius;
		}
	}
}