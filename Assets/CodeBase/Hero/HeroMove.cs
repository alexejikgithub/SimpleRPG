using SimpleRPG.Data;
using SimpleRPG.Infrastructure.Services;
using SimpleRPG.Services.Input;
using SimpleRPG.Services.PersistantProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimpleRPG.Hero
{
	public class HeroMove : MonoBehaviour, ISaveProgress, ISavedProgressReader
	{
		[SerializeField] private CharacterController _characterController;
		[SerializeField] private float _movementSpeed;

		private IInputService _inputService;
		private Camera _camera;

		private Vector3 _movementVector;



		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
			_camera = Camera.main;
		}


		private void Update()
		{
			_movementVector = Vector3.zero;
			if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
			{
				_movementVector = _camera.transform.TransformDirection(_inputService.Axis);
				_movementVector.y = 0;
				_movementVector.Normalize();

				transform.forward = _movementVector;
			}

			_movementVector += Physics.gravity;
			_characterController.Move(_movementVector * (_movementSpeed * Time.deltaTime));
		}

		public void UpdateProgress(PlayerProgress progress)
		{
			progress.WorldData.PositionOnLevel = new PositionOnLevel(GetCurrentLevel(), transform.position.AsVectorData());
		}

		public void LoadProgress(PlayerProgress progress)
		{
			if (progress.WorldData.PositionOnLevel.Level == GetCurrentLevel())
			{
				Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
				if (savedPosition != null)
				{
					Warp(to: savedPosition);
				}
			}
		}

		private void Warp(Vector3Data to)
		{
			_characterController.enabled = false;
			transform.position = to.AsUnityVector().AddY(_characterController.height);
			_characterController.enabled = true;
		}

		private string GetCurrentLevel()
		{
			return SceneManager.GetActiveScene().name;
		}
	}
}
