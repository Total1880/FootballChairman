using FootballChairman.Models;
using FootballChairman.Repositories;
using FootballChairman.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IRepository<Manager> _managerRepository;

        public ManagerService(IRepository<Manager> managerRepository)
        {
            _managerRepository = managerRepository;
        }
        public Manager CreateManager(Manager manager)
        {
            var list = _managerRepository.Get();
            if (!list.Any(m => m.Id == manager.Id))
            {
                list.Add(manager);
            }
            _managerRepository.Create(list);
            return manager;
        }

        public IList<Manager> GetAllManagers()
        {
            return _managerRepository.Get();
        }

        public Manager GetManager(int id)
        {
            return _managerRepository.Get().FirstOrDefault(m => m.Id == id);

        }
    }
}
