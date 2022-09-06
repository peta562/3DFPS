using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor {
    public static class SaveLoadMenu {
        [MenuItem("GameTools/Save/Open Directory")]
        static void OpenDirectory() {
            Process.Start(Application.persistentDataPath);
        }
        
        [MenuItem("GameTools/Save/ClearSave")]
        static void ClearSave() {
            File.Delete(Application.persistentDataPath + "/Save.json");
        }
    }
}