﻿using System.Threading.Tasks;
using Infrastructure.Services.Ads;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Configs;
using Infrastructure.Services.IAP;
using Infrastructure.Services.PauseService;
using Infrastructure.Services.SaveDataHandler;
using Infrastructure.Services.WindowService;
using UI.Windows;
using UI.Windows.Shop;
using UnityEngine;

namespace Infrastructure.Services.UIFactory {
    public sealed class UIFactory : IUIFactory {
        readonly IAssetProvider _assetProvider;
        readonly IConfigProvider _configProvider;
        readonly ISaveDataHandler _saveDataHandler;
        readonly IPauseService _pauseService;
        readonly IAdService _adService;
        readonly IIAPService _iapService;

        Transform _uiRoot;

        public UIFactory(IAssetProvider assetProvider, IConfigProvider configProvider,
            ISaveDataHandler saveDataHandler, IPauseService pauseService, IAdService adService,
            IIAPService iapService) {
            _assetProvider = assetProvider;
            _configProvider = configProvider;
            _saveDataHandler = saveDataHandler;
            _pauseService = pauseService;
            _adService = adService;
            _iapService = iapService;
        }

        public void CreateShop() {
            var config = _configProvider.GetWindowConfig(WindowType.ShopWindow);

            var window = Object.Instantiate(config.Prefab, _uiRoot) as ShopWindow;
            if ( window != null ) {
                window.Init(_adService, _saveDataHandler, _pauseService, _iapService, _assetProvider);
            }
        }

        public async Task CreateUIRoot() {
            var uiRootObject = await _assetProvider.Instantiate(AssetAddress.UICanvas);
            _uiRoot = uiRootObject.transform;
        }
    }
}