using System.Collections.Generic;
using Data;

namespace Infrastructure.Services.SaveDataHandler {
    public class SaveDataHandler : ISaveDataHandler {
        public SaveData SaveData { get; set; }
        public HashSet<ISaveWriter> SaveWriters { get; } = new HashSet<ISaveWriter>();
        public HashSet<ISaveReader> SaveReaders { get; } = new HashSet<ISaveReader>();

        public void RegisterSaveWriter(ISaveWriter saveWriter) {
            SaveWriters.Add(saveWriter);
        }

        public void RegisterSaveReader(ISaveReader saveReader) {
            SaveReaders.Add(saveReader);
        }
    }
}