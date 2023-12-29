using SimpleRPG.CameraLogic;
using SimpleRPG.Infrastructure;
using SimpleRPG.Services.Input;
using UnityEngine;

namespace SimpleRPG.Hero
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _movementSpeed;
        
        private IInputService _inputService;
        private Camera _camera;

        private Vector3 _movementVector;

        private void Awake()
        {
            _inputService = Game.InputService;
        }

        private void Start()
        {
            _camera = Camera.main;
            StartCameraFollow();
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
            _characterController.Move( _movementVector * (_movementSpeed * Time.deltaTime));
        }
        
        private void StartCameraFollow() => _camera.GetComponent<CameraFollow>().Follow(gameObject);
    }
}
