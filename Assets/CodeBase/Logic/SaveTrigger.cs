using SimpleRPG.Infrastructure.Services;
using SimpleRPG.Infrastructure.Services.SaveLoad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SimpleRPG.Logic
{
	internal class SaveTrigger:MonoBehaviour
	{
		[SerializeField] private BoxCollider _boxCollider;
		private ISaveLoadService _saveLoadService;

		private void Awake()
		{
			_saveLoadService = AllServices.Container.Single<ISaveLoadService>();
		}

		private void OnTriggerEnter(Collider other)
		{
			_saveLoadService.SaveProgress();
			Debug.Log("Progress Saved.");
			gameObject.SetActive(false);
		}

		private void OnDrawGizmos()
		{
			if (_boxCollider == null)
				return;
			Gizmos.color = new Color(30, 200, 30, 130);
			Gizmos.DrawCube(transform.position +_boxCollider.center, _boxCollider.size);
		}
	}
}
