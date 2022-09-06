using UnityEngine;

namespace Infrastructure.Services.AssetManagement {
    public sealed class AssetProvider : IAssetProvider {
        public GameObject Instantiate(string path, Vector3 at) {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public GameObject Instantiate(string path) {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
    }
}