using System;
using System.Linq;
using SimpleRPG.Hero;
using SimpleRPG.Infrastructure.Factory;
using SimpleRPG.Infrastructure.Services;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace SimpleRPG.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _attackCooldown = 2f;
        [SerializeField] private float _hitboxRadius = 0.5f;
        [SerializeField] private float _attackDistance = 0.5f;
        [SerializeField] private float _damage=10;

        private IGameFactory _factory;
        private Transform _heroTransform;
        private float _currentAttackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _isAttackActive;
        

        private void Awake()
        {
            _factory = AllServices.Container.Single<IGameFactory>();
            _factory.HeroCreated += OnHeroCreated;

            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            UpdateCooldown();
            if (CanAttack())
            {
                StartAttack();
            }
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(StartPoint(), _hitboxRadius, 1);

                if (hit.TryGetComponent<HeroHealth>(out HeroHealth heroHealth))
                {
                    heroHealth.TakeDamage(_damage);
                }
            }
        }

        private bool Hit(out Collider hit)
        {
            int hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), _hitboxRadius, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitsCount > 0;
        }

        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
                   transform.forward * _attackDistance;
        }

        private void OnAttackEnded()
        {
            _currentAttackCooldown = _attackCooldown;
            _isAttacking = false;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
            {
                _currentAttackCooldown -= Time.deltaTime;
            }
        }

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _animator.PlayAttack();

            _isAttacking = true;
        }

        private bool CanAttack()
        {
            return _isAttackActive && !_isAttacking && CooldownIsUp();
        }

        private bool CooldownIsUp()
        {
            return _currentAttackCooldown <= 0;
        }

        private void OnHeroCreated() =>
            _heroTransform = _factory.HeroGameObject.transform;

        public void Disable()
        {
            _isAttackActive = false;
        }

        public void Enable()
        {
            _isAttackActive = true;
        }
    }
}