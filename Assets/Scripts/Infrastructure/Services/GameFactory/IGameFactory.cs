using GameCore.CommonLogic;
using GameCore.Loot;
using UnityEngine;

namespace Infrastructure.Services.GameFactory {
    public interface IGameFactory : IService {
        GameObject CreatePlayer(Vector3 position);
        GameObject CreateHUD();
        GameObject CreateEnemy(EnemyTypeId enemyTypeId, Transform parent);
        GameObject CreateWeapon(WeaponTypeId weaponTypeId, Transform parent);
        LootPiece CreateLoot(int lootMin, int lootMax);
        void CreateSpawner(Vector3 position, float spawnTime);
    }
}