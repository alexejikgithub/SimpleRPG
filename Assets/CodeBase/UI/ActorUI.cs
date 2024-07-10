using SimpleRPG.Hero;
using UnityEngine;

namespace SimpleRPG.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;
 
        private HeroHealth _heroHealth;

        private void OnDestroy()
        {
            _heroHealth.HeathChanged -= UpdateHpBar;
        }

        public void Construct(HeroHealth heroHealth)
        {
            _heroHealth = heroHealth;
            _heroHealth.HeathChanged += UpdateHpBar;
        }

        private void UpdateHpBar()
        {
            _hpBar.SetValue(_heroHealth.Current, _heroHealth.Max);
        }
    }
}