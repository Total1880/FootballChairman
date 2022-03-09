using FootballChairman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services.Interfaces
{
    public interface IManagerService
    {
        Manager CreateManager(Manager manager);
        Manager GenerateManager(int clubId);
        Manager GetManager(int id);
        IList<Manager> GetAllManagers();
        IList<Manager> UpdateManagersEndSeason();

    }
}
