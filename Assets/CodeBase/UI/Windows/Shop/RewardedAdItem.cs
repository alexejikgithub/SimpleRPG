using SimpleRPG.Infrastructure;
using SimpleRPG.Infrastructure.Services.Ads;
using SimpleRPG.Services.PersistantProgress;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleRPG.UI.Windows.Shop
{
    public class RewardedAdItem : MonoBehaviour
    {
        [SerializeField] private Button _showAdButton;
        [SerializeField] private GameObject[] _adActiveObjects;
        [SerializeField] private GameObject[] _adInactiveObjects;
        private IAdsService _adsService;
        private IPersistantProgressService _progressService;

        public void Construct(IAdsService adsService, IPersistantProgressService progressService)
        {
            _adsService = adsService;
            _progressService = progressService;
        }

        public void Initialize()
        {
            _showAdButton.onClick.AddListener(OnShowAddCkicked);
            RefreshAvailableAd();
        }

        public void Subscribe()
        {
            _adsService.RewardedVideoReady += RefreshAvailableAd;
        }


        public void Cleanup()
        {
            _adsService.RewardedVideoReady -= RefreshAvailableAd;
        }

        private void OnShowAddCkicked()
        {
            _adsService.ShowRewardedVideo(OnVideoFinished);
        }

        private void OnVideoFinished()
        {
            _progressService.PlayerProgress.WorldData.LootData.Add(_adsService.Reward);
        }

        private void RefreshAvailableAd()
        {
            bool isVideoReady = _adsService.IsRewardedVideoReady;
            foreach (var adActiveObject in _adActiveObjects)
            {
                adActiveObject.SetActive(isVideoReady);
            }
            foreach (var adInactiveObject in _adInactiveObjects)
            {
                adInactiveObject.SetActive(!isVideoReady);
            }
        }
    }
}