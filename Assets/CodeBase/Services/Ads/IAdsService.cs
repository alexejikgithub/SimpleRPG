using System;
using SimpleRPG.Enemy;

namespace SimpleRPG.Infrastructure.Services.Ads
{
    public interface IAdsService: IService
    {
        event Action RewardedVideoReady;

        void InitializeAds();
        void ShowRewardedVideo(Action onVideoFinished);
        bool IsRewardedVideoReady { get; }
        int Reward { get;}
    }
}