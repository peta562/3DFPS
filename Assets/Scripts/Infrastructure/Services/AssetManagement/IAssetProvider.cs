using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.Services.AssetManagement {
    public interface IAssetProvider : IService {
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path);
        void Init();
        Task<T> Load<T>(AssetReference assetReference) where T : class;
        Task<T> Load<T>(string address) where T : class;
        void CleanUp();
    }
}