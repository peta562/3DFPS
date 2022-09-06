﻿using UnityEngine;

namespace Infrastructure.Services.AssetManagement {
    public interface IAssetProvider : IService {
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path);
    }
}