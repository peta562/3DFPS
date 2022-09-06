using System.Collections.Generic;
using Data;

namespace Infrastructure.Services.SaveDataHandler {
    public interface ISaveDataHandler : IService {
        SaveData SaveData { get; set; }
        HashSet<ISaveWriter> SaveWriters { get; }
        HashSet<ISaveReader> SaveReaders { get; }

        void RegisterSaveWriter(ISaveWriter saveWriter);
        void RegisterSaveReader(ISaveReader saveReader);
    }
}