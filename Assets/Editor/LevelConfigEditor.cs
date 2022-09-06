using System;
using System.Linq;
using Configs;
using Configs.LevelConfig;
using GameCore.CommonLogic.EnemySpawners;
using Infrastructure;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor {
    [CustomEditor(typeof(LevelConfig))]
    public sealed class LevelConfigEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            var levelConfig = (LevelConfig) target;
            
            if ( GUILayout.Button("Collect") ) {
                levelConfig.EnemySpawnerConfigs = FindObjectsOfType<SpawnMarker>()
                    .Select(x => new EnemySpawnerConfig(x.transform.position))
                    .ToList();

                var sceneNameStr = SceneManager.GetActiveScene().name;
                if ( !Enum.TryParse(sceneNameStr, out SceneName sceneName) ) {
                    Debug.LogError("Can't parse scene name to enum");
                }
                levelConfig.SceneName = sceneName;
            }
            
            EditorUtility.SetDirty(target);
        }
    }
}