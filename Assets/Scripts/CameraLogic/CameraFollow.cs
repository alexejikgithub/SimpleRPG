using System;
using SimpleRPG.Infrastructure;
using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleRPG.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        [SerializeField] private float _rotationAngleX;
        [SerializeField] private int _distance;
        [SerializeField] private float _offcetY;

        
        private void LateUpdate()
        {
            if (_target == null)
                return;

            Quaternion rotation = Quaternion.Euler(_rotationAngleX, 0, 0);
            Vector3 position = rotation * new Vector3(0, 0, -_distance) + GetTargetPosition();

            transform.rotation = rotation;
            transform.position = position;
        }

        public void Follow(GameObject target)
        {
            _target = target.transform;
        }
        
        private Vector3 GetTargetPosition()
        {
            Vector3 targetPosition = _target.position;
            targetPosition.y += _offcetY;
            return targetPosition;
        }
    }
}