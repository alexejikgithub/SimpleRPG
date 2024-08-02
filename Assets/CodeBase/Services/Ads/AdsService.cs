using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace SimpleRPG.Infrastructure.Services.Ads
{
    public class AdsService : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener, IAdsService
    {
        public event Action RewardedVideoReady;
        private Action _videoFinished;
        
        private const string AndroidGameId = "Secret";
        private const string IOSGameId = "Secret";

        private const string RewardedVideoPlacementId = "Rewarded_Android";
        public int Reward => 13;  // Testing value

        private bool _testMode = true;
        private string _gameId;

        void Awake()
        {
            InitializeAds();
        }

        public void InitializeAds()
        {
#if UNITY_IOS
            _gameId = IOSGameId;
#elif UNITY_ANDROID
            _gameId = AndroidGameId;
#elif UNITY_EDITOR
            _gameId = AndroidGameId; //Only for testing the functionality in the Editor
#endif
            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(_gameId, _testMode, this);
            }
        }

        public void LoadRewardedVideo()
        {
            if (Advertisement.isInitialized)
            {
                Advertisement.Load(RewardedVideoPlacementId, this);
            }
        }

        public void ShowRewardedVideo(Action onVideoFinished)
        {
            if (Advertisement.isInitialized)
            {
                _videoFinished = onVideoFinished;
                Advertisement.Show(RewardedVideoPlacementId, this);
            }
            else
            {
                Debug.Log("Rewarded video is not ready yet.");
                LoadRewardedVideo();  // Try to load the video again
            }
        }

        public bool IsRewardedVideoReady => Advertisement.isInitialized;

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
            LoadRewardedVideo();  // Load the rewarded video ad after initialization
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log($"OnUnityAdsAdLoaded {placementId}");
            if (placementId == RewardedVideoPlacementId)
            {
                RewardedVideoReady?.Invoke();
            }
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Unity Ads Failed to Load: {placementId} - {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.Log(error);
            Debug.Log(message);
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            Debug.Log($"OnUnityAdsShowStart {placementId}");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            Debug.Log($"OnUnityAdsShowClick {placementId}");
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            switch (showCompletionState)
            {
                case UnityAdsShowCompletionState.SKIPPED:
                    Debug.Log($"UnityAdsShowComplete {showCompletionState}");
                    break;
                case UnityAdsShowCompletionState.COMPLETED:
                    Debug.Log($"UnityAdsShowComplete {showCompletionState}");
                    _videoFinished?.Invoke();
                    break;
                case UnityAdsShowCompletionState.UNKNOWN:
                    Debug.Log($"UnityAdsShowComplete {showCompletionState}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(showCompletionState), showCompletionState, null);
            }

            _videoFinished = null;
        }
    } 
}
