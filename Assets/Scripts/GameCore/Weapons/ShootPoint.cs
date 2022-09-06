using UnityEngine;

namespace GameCore.Weapons {
    public sealed class ShootPoint : MonoBehaviour {
        void OnGUI() {
            var size = 24;
            float posX = GetComponent<Camera>().pixelWidth / 2 - size / 4;
            float posY = GetComponent<Camera>().pixelHeight / 2 - size / 2;
            GUI.Label(new Rect(posX, posY, size, size), "+");
        }
    }
}