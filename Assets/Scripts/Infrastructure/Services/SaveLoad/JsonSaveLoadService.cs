using System.IO;
using Data;
using Infrastructure.Services.SaveDataHandler;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Services.SaveLoad {
    public sealed class JsonSaveLoadService : ISaveLoadService {
        readonly string _filePath;

        readonly ISaveDataHandler _saveDataHandler;
        
        public JsonSaveLoadService(ISaveDataHandler saveDataHandler) {
            _saveDataHandler = saveDataHandler;
            
            _filePath = Application.persistentDataPath + "/Save.json";
        }

        public void SaveData() {
            foreach (var saveWriter in _saveDataHandler.SaveWriters) {
                saveWriter.SaveData(_saveDataHandler.SaveData);
            }
            
            var json = JsonUtility.ToJson(_saveDataHandler.SaveData);

            using (var writer = new StreamWriter(_filePath)) {
                writer.Write(json);
            }
        }

        [CanBeNull]
        public SaveData LoadData() {
            var json = "";

            if ( !File.Exists(_filePath) ) {
                return null;
            }

            using (var reader = new StreamReader(_filePath)) {
                string line;

                while ( (line = reader.ReadLine()) != null ) {
                    json += line;
                }
            }

            if ( string.IsNullOrEmpty(json) ) {
                return null;
            }

            return JsonUtility.FromJson<SaveData>(json);
        }
    }
}