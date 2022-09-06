using System.Collections.Generic;
using UnityEngine;

namespace Configs.Windows {
    [CreateAssetMenu(fileName = "WindowConfig", menuName = "Configs/WindowConfig", order = 3)]
    public class WindowConfig : ScriptableObject {
        public List<WindowDescription> WindowDescriptions;
    }
}