using System;
using SimpleRPG.Hero;
using SimpleRPG.Logic;
using UnityEngine;

namespace SimpleRPG.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;
 
        private IHealth _health;

        private void OnDestroy()
        {
            if (_health != null)
            {
                _health.HealthChanged -= UpdateHpBar;
            }
        }

        private void Start()
        {
            //TODO Move to factory later
            if(_health!=null)
                return;
            IHealth health = GetComponent<IHealth>();
            if (health != null)
            {
                Construct(health);
            }
        }

        public void Construct(IHealth heroHealth)
        {
            _health = heroHealth;
            _health.HealthChanged += UpdateHpBar;
        }

        private void UpdateHpBar()
        {
            _hpBar.SetValue(_health.Current, _health.Max);
        }
    }
}