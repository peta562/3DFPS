using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Infrastructure.Services.AssetManagement {
    public sealed class AssetProvider : IAssetProvider {
        readonly Dictionary<string, AsyncOperationHandle> _completedCache =
            new Dictionary<string, AsyncOperationHandle>();

        readonly Dictionary<string, List<AsyncOperationHandle>> _handles =
            new Dictionary<string, List<AsyncOperationHandle>>();

        public Task<GameObject> Instantiate(string address, Vector3 at) =>
            Addressables.InstantiateAsync(address, at, Quaternion.identity).Task;

        public Task<GameObject> Instantiate(string address, Transform parent) => 
            Addressables.InstantiateAsync(address, parent).Task;

        public Task<GameObject> Instantiate(string address) => 
            Addressables.InstantiateAsync(address).Task;

        public void Init() {
            Addressables.InitializeAsync();
        }

        public async Task<T> Load<T>(AssetReference assetReference) where T : class {
            if ( _completedCache.TryGetValue(assetReference.AssetGUID, out var completedHandle) ) {
                return completedHandle.Result as T;
            }

            var handle = Addressables.LoadAssetAsync<T>(assetReference);

            return await RunWithCacheOnComplete(handle, assetReference.AssetGUID);
        }

        public async Task<T> Load<T>(string address) where T : class {
            if ( _completedCache.TryGetValue(address, out var completedHandle) ) {
                return completedHandle.Result as T;
            }

            var handle = Addressables.LoadAssetAsync<T>(address);

            return await RunWithCacheOnComplete(handle, address);
        }

        public void CleanUp() {
            foreach (var resourceHandle in _handles.Values) {
                foreach (var handle in resourceHandle) {
                    Addressables.Release(handle);
                }
            }

            _completedCache.Clear();
            _handles.Clear();
        }

        void AddHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class {
            if ( !_handles.TryGetValue(key, out var resourceHandle) ) {
                resourceHandle = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandle;
            }

            resourceHandle.Add(handle);
        }

        async Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class {
            handle.Completed += h => { _completedCache[cacheKey] = h; };

            AddHandle(cacheKey, handle);

            return await handle.Task;
        }
    }
}