using Data;

namespace Infrastructure.Services.SaveDataHandler {
    public interface ISaveReader {
        void LoadSave(SaveData saveData);
    }
}