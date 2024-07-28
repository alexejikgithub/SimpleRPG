using System;
using SimpleRPG.Data;
using SimpleRPG.Infrastructure.Services;
using SimpleRPG.Logic;
using SimpleRPG.Services.Input;
using SimpleRPG.Services.PersistantProgress;
using UnityEngine;

namespace SimpleRPG.Hero
{
    [RequireComponent(typeof(HeroAnimator),typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _weaponTransform;
        
        private IInputService _input;

        private int _layerMask;
        private float _radius;
        private Collider[] _hits = new Collider[3];
        private Stats _stats;

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>(); //TODO refactor later
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (_input.IsAttackButtonUp() && !_heroAnimator.IsAttacking)
            {
                _heroAnimator.PlayAttack();
            }
        }

        public void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
            {
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _stats = progress.HeroStats;
        }

        private int Hit()
        {
          var hitCount =  Physics.OverlapSphereNonAlloc(_weaponTransform.position, _stats.DamageRadius, _hits, _layerMask);
          return hitCount;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color=Color.white;
            Gizmos.DrawSphere(_weaponTransform.position,1f);
        }

        
    }
}