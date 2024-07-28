using System.Linq;
using SimpleRPG.Logic;
using UnityEngine;

namespace SimpleRPG.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _attackCooldown = 2f;
        [SerializeField] private float _hitboxRadius = 0.5f;
        [SerializeField] private float _damage = 10;
        [SerializeField] private Transform _weaponTransform;

        private Transform _heroTransform;
        private float _currentAttackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _isAttackActive;


        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }

        private void Awake()
        {

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

        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(_weaponTransform.position, _hitboxRadius, 1);

                if (hit.TryGetComponent<IHealth>(out IHealth heroHealth))
                {
                    heroHealth.TakeDamage(_damage);
                }
            }
        }

        private bool Hit(out Collider hit)
        {
            int hitsCount = Physics.OverlapSphereNonAlloc(_weaponTransform.position, _hitboxRadius, _hits, _layerMask);
            hit = _hits.FirstOrDefault();

            return hitsCount > 0;
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