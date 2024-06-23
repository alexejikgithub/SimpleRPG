using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SimpleRPG.Enemy
{
	public class Agro : MonoBehaviour
	{
		[SerializeField] private TriggerObserver _triggerObserver;
		[SerializeField] private Follow _movement;
		[SerializeField] private float _cooldown;

		private Coroutine _agroCoroutine;
		private bool _hasAgroTarget;

		private void Start()
		{
			_triggerObserver.TriggerEnter += TriggerEnter;
			_triggerObserver.TriggerExit += TriggerExit;

			DisableFollow();
		}

		private void TriggerExit(Collider collider)
		{
			if (_hasAgroTarget)
			{
				_hasAgroTarget = false;
				_agroCoroutine = StartCoroutine(StopFollowOnCooldown());
			}
		}

		private void TriggerEnter(Collider collider)
		{
			if (!_hasAgroTarget)
			{
				_hasAgroTarget = true;
				StopAgroCoroutine();
				EnableFollow();
			}

		}

		private IEnumerator StopFollowOnCooldown()
		{
			yield return new WaitForSeconds(_cooldown);
			DisableFollow();
		}

		private void StopAgroCoroutine()
		{
			if(_agroCoroutine!=null)
			{
				StopCoroutine(_agroCoroutine);
				_agroCoroutine = null;
			}
		}

		private void EnableFollow()
		{
			_movement.enabled = true;
		}

		private void DisableFollow()
		{ 
		_movement.enabled = false;
		}
	}
}