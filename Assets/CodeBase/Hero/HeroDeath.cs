using UnityEngine;

namespace SimpleRPG.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroMove _move;
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private GameObject _deathFx;
        private bool _isDead;

        private void Start()
        {
            _health.HealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (!_isDead && _health.Current <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _isDead = true;
            _move.enabled = false;
            _attack.enabled = false;
            _animator.PlayDeath();

            Instantiate(_deathFx, transform.position, Quaternion.identity);
        }
    }
}