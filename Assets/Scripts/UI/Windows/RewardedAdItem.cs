using Infrastructure.Services.Ads;
using Infrastructure.Services.SaveDataHandler;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows {
    public sealed class RewardedAdItem : MonoBehaviour {
        [SerializeField] Button ShowAdButton;

        IAdService _adService;
        ISaveDataHandler _saveDataHandler;

        public void Construct(IAdService adService, ISaveDataHandler saveDataHandler) {
            _adService = adService;
            _saveDataHandler = saveDataHandler;
        }
        
        public void Init() {
            ShowAdButton.onClick.AddListener(OnShowAdButtonClicked);
            RefreshAvailableAd();
        }
        
        public void Subscribe() => 
            _adService.RewardedVideoReady += RefreshAvailableAd;

        public void Unsubscribe() => 
            _adService.RewardedVideoReady -= RefreshAvailableAd;

        void OnShowAdButtonClicked() {
            _adService.ShowRewardedVideo(OnVideoFinished);
        }

        void OnVideoFinished() {
            _saveDataHandler.SaveData.PlayerSaveData.PlayerLootData.Collect(_adService.Reward);
        }

        void RefreshAvailableAd() {
            var adReady = _adService.IsRewardedVideoReady();

            ShowAdButton.interactable = adReady;
        }
    }
}