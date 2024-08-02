using System;
using SimpleRPG.Data;
using SimpleRPG.Services.PersistantProgress;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleRPG.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] protected Button CloseButton;

        protected IPersistantProgressService ProgressService;
        protected PlayerProgress Progress => ProgressService.PlayerProgress;

        public void Construct(IPersistantProgressService progressService)
        {
            ProgressService = progressService;
        }
        
        private void Awake()
        {
            OnAwake();
        }

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy()
        {
            Cleanup();
        }

        protected virtual void OnAwake()
        {
            CloseButton.onClick.AddListener(()=> Destroy(gameObject));
        }

        protected virtual void Initialize()
        {
        }
        
        protected virtual void SubscribeUpdates()
        {
        }

        protected virtual void Cleanup()
        {
        }
        
    }
}