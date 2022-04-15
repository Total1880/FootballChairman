using FootballChairman.Models;
using FootballChairman.Repositories;
using FootballChairman.Services.Interfaces;

namespace FootballChairman.Services
{
    public class SaveGameDataService : ISaveGameDataService
    {
        private readonly IRepository<SaveGameData> _saveGameDataRepository;

        public SaveGameDataService(IRepository<SaveGameData> saveGameDataRepository)
        {
            _saveGameDataRepository = saveGameDataRepository;
        }

        public SaveGameData CreateSaveGameData(SaveGameData data)
        {
            var list = _saveGameDataRepository.Get();
            list.Remove(list.FirstOrDefault(s => s.Name == data.Name));
            list.Add(data);
            _saveGameDataRepository.Create(list);

            return data;
        }

        public bool DoesSaveGameExist(string name)
        {
            return _saveGameDataRepository.Get().FirstOrDefault(s => s.Name == name) != null;
        }

        public SaveGameData GetSaveGameData(string name)
        {
            var data = _saveGameDataRepository.Get().FirstOrDefault(s => s.Name == name);
            if (data == null)
            {
                return CreateSaveGameData(new SaveGameData { Name = name, MatchDay = 0, Year = 1 });
            }
            return data;
        }

        
    }
}
