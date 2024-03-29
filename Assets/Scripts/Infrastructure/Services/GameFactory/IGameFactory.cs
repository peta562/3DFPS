﻿using System.Threading.Tasks;
using GameCore.CommonLogic;
using GameCore.Loot;
using UI;
using UnityEngine;

namespace Infrastructure.Services.GameFactory {
    public interface IGameFactory : IService {
        Task WarmUp();
        Task<GameObject> CreatePlayer(Vector3 position);
        Task<GameObject> CreateHUD();
        Task<GameObject> CreateEnemy(EnemyTypeId enemyTypeId, Transform parent);
        GameObject CreateWeapon(WeaponTypeId weaponTypeId, Transform parent);
        Task<LootPiece> CreateLoot(int lootMin, int lootMax);
        Task CreateSpawner(Vector3 position, float spawnTime);
        Task<MainMenuUI> CreateMainMenuUI();
        void Cleanup();
    }
}