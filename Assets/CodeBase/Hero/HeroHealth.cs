using System;
using SimpleRPG.Data;
using SimpleRPG.Services.PersistantProgress;
using UnityEngine;

namespace SimpleRPG.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISaveProgress, ISavedProgressReader
    {
        public Action HeathChanged;

        [SerializeField] private HeroAnimator _heroAnimator;
        private State _state;

        public float Current
        {
            get => _state.CurrentHP;
            set
            {
                if (_state.CurrentHP != value)
                {
                    HeathChanged?.Invoke();
                    _state.CurrentHP = value;
                }
            }
        }

        public float Max
        {
            get => _state.MaxHP;
            set => _state.MaxHP = value;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HeroState;
            HeathChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHP = Current;
            progress.HeroState.MaxHP = Max;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
            {
                return;
            }

            Current -= damage;
            _heroAnimator.PlayHit();
        }
    }
}