using System;
using UnityEngine;

namespace SimpleRPG.Enemy
{
	[RequireComponent(typeof(Collider))]

	public class TriggerObserver: MonoBehaviour
	{
		public event Action <Collider> TriggerEnter;
		public event Action <Collider> TriggerExit;

		private void OnTriggerEnter(Collider other)
		{
			TriggerEnter?.Invoke(other);
		}
		public void OnTriggerExit(Collider other)
		{
			TriggerExit?.Invoke(other);
		}
	}
}