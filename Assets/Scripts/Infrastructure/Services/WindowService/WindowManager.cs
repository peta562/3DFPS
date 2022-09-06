using System;
using Infrastructure.Services.UIFactory;

namespace Infrastructure.Services.WindowService {
    public sealed class WindowManager : IWindowManager {
        readonly IUIFactory _uiFactory;

        public WindowManager(IUIFactory uiFactory) {
            _uiFactory = uiFactory;
        }

        public void Show(WindowType windowType) {
            switch (windowType) {
                case WindowType.None:
                    break;
                case WindowType.ShopWindow:
                    _uiFactory.CreateShop();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(windowType), windowType, null);
            }
        }
    }
}