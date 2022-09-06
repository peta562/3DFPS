using Infrastructure.Services.Ads;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Configs;
using Infrastructure.Services.PauseService;
using Infrastructure.Services.SaveDataHandler;
using Infrastructure.Services.WindowService;
using UI.Windows;
using UnityEngine;

namespace Infrastructure.Services.UIFactory {
    public sealed class UIFactory : IUIFactory {
        readonly IAssetProvider _assetProvider;
        readonly IConfigProvider _configProvider;
        readonly ISaveDataHandler _saveDataHandler;
        readonly IPauseService _pauseService;
        readonly IAdService _adService;

        Transform _uiRoot;

        public UIFactory(IAssetProvider assetProvider, IConfigProvider configProvider,
            ISaveDataHandler saveDataHandler, IPauseService pauseService, IAdService adService) {
            _assetProvider = assetProvider;
            _configProvider = configProvider;
            _saveDataHandler = saveDataHandler;
            _pauseService = pauseService;
            _adService = adService;
        }

        public void CreateShop() {
            var config = _configProvider.GetWindowConfig(WindowType.ShopWindow);

            var window = Object.Instantiate(config.Prefab, _uiRoot) as ShopWindow;
            if ( window != null ) {
                window.Init(_adService, _saveDataHandler, _pauseService);
            }
        }

        public void CreateUIRoot() {
            _uiRoot = _assetProvider.Instantiate("UI/UICanvas").transform;
        }
    }
}