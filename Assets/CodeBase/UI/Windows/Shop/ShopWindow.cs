using SimpleRPG.Infrastructure.Services.Ads;
using SimpleRPG.Services.PersistantProgress;
using TMPro;
using UnityEngine;

namespace SimpleRPG.UI.Windows.Shop
{
    public class ShopWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _currencyText;
        [SerializeField] private RewardedAdItem _adItem;

        public void Construct(IAdsService adsService,IPersistantProgressService progressService)
        {
            base.Construct(progressService);
            _adItem.Construct(adsService, progressService);
        }
        
        protected override void Initialize()
        {
            _adItem.Initialize();
            RefreshCurrencyText();
        }
 

        protected override void SubscribeUpdates()
        {
            _adItem.Subscribe();
            Progress.WorldData.LootData.Changed += RefreshCurrencyText;
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _adItem.Cleanup();
            Progress.WorldData.LootData.Changed-= RefreshCurrencyText;
        }
        
        private void RefreshCurrencyText()
        {
            _currencyText.text = Progress.WorldData.LootData.CollectedAmount.ToString();
        }
    }
}