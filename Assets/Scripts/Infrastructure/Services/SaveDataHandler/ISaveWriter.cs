using Data;

namespace Infrastructure.Services.SaveDataHandler {
    public interface ISaveWriter {
        void SaveData(SaveData saveData);
    }
}