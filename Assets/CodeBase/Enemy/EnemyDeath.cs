using System;
using System.Collections;
using SimpleRPG.Hero;
using UnityEngine;

namespace SimpleRPG.Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        public event Action Happaned;
        
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private Follow _follow;

        [SerializeField] private GameObject _deathFx;

        private void Start()
        {
            _health.HealthChanged += HealthChanged;
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= HealthChanged;        
        }

        private void HealthChanged()
        {
            if (_health.Current <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _health.HealthChanged -= HealthChanged;    
            _animator.PlayDeath();
            _follow.enabled = false;
            SpawnFx();
            StartCoroutine(DestroyCorpse());
            Happaned?.Invoke();
        }

        private void SpawnFx()
        {
            Instantiate(_deathFx,transform.position,Quaternion.identity);

        }

        private IEnumerator DestroyCorpse()
        {
            yield return new WaitForSeconds(1.5f);
            Destroy(gameObject);
        }
    }
}