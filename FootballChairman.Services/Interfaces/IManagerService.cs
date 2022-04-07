using FootballChairman.Models;

namespace FootballChairman.Services.Interfaces
{
    public interface IManagerService
    {
        Manager CreateManager(Manager manager);
        Manager GenerateManager(int clubId, int countryId);
        Manager GetManager(int id);
        Manager GetManagerFromClub(int clubId);
        IList<Manager> GetAllManagers();
        IList<Manager> UpdateManagersEndSeason();
        Manager UpdateManager(Manager manager);
        void DoTransfers();
    }
}
