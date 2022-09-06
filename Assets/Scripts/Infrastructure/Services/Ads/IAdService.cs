using System;

namespace Infrastructure.Services.Ads {
    public interface IAdService : IService {
        event Action RewardedVideoReady;
        int Reward { get; }
        void Init();
        void ShowRewardedVideo(Action onVideoFinished);
        bool IsRewardedVideoReady();
    }
}