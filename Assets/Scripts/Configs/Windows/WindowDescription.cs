using System;
using Infrastructure.Services.WindowService;
using UI.Windows;

namespace Configs.Windows {
    [Serializable]
    public class WindowDescription {
        public WindowType WindowType;
        public BaseWindow Prefab;
    }
}