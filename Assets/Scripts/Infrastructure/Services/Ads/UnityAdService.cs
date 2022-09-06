using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Infrastructure.Services.Ads {
    public sealed class UnityAdService : IUnityAdsListener, IAdService {
        const string AndroidGameId = "4916005";
        const string IOSGameId = "4916004";
        const string RewardedVideoPlacementId = "Rewarded_Android";
        
        public event Action RewardedVideoReady;
        
        public int Reward => 15;
        
        string _gameId;
        Action _onVideoFinished;
        
        public void Init() {
            switch (Application.platform) {
                case RuntimePlatform.Android:
                    _gameId = AndroidGameId;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    _gameId = IOSGameId;
                    break;
                case RuntimePlatform.WindowsEditor:
                    _gameId = AndroidGameId;
                    break;
                default:
                    Debug.LogError("Unsupported platform for ads");
                    break;
            }

            Advertisement.AddListener(this);
            Advertisement.Initialize(_gameId);
        }

        public void ShowRewardedVideo(Action onVideoFinished) {
            _onVideoFinished = onVideoFinished;
            
            Advertisement.Show(RewardedVideoPlacementId);
        }

        public bool IsRewardedVideoReady() =>
            Advertisement.IsReady(RewardedVideoPlacementId);
        
        public void OnUnityAdsReady(string placementId) {
            Debug.Log($"Unity ads ready {placementId}");

            if ( placementId == RewardedVideoPlacementId ) {
                RewardedVideoReady?.Invoke();
            }
        }

        public void OnUnityAdsDidError(string message) {
            Debug.LogError($"Unity ads error {message}");
        }

        public void OnUnityAdsDidStart(string placementId) {
            Debug.Log($"Unity ads start {placementId}");
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) {
            switch (showResult) {
                case ShowResult.Failed:
                    Debug.LogError($"Unity ads failed to finish");
                    break;
                case ShowResult.Skipped:
                    Debug.LogError($"Unity ads skipped");
                    break;
                case ShowResult.Finished:
                    _onVideoFinished?.Invoke();
                    break;
                default:
                    Debug.LogError($"Unity ads unknown result");
                    break;
            }

            _onVideoFinished = null;
        }
    }
}