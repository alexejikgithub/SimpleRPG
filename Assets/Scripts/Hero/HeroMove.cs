using SimpleRPG.CameraLogic;
using SimpleRPG.Infrastructure;
using SimpleRPG.Infrastructure.Services;
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
        

        private void Start()
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
            _characterController.Move( _movementVector * (_movementSpeed * Time.deltaTime));
        }
        
    }
}
