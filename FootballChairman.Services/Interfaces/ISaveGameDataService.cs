using FootballChairman.Models;

namespace FootballChairman.Services.Interfaces
{
    public interface ISaveGameDataService
    {
        SaveGameData GetSaveGameData(string name);
        SaveGameData CreateSaveGameData(SaveGameData data);
    }
}
