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
        private readonly IPersonNameService _personNameService;
        private Random random = new Random();

        public ManagerService(IRepository<Manager> managerRepository, IPersonNameService personNameService)
        {
            _managerRepository = managerRepository;
            _personNameService = personNameService;
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

        public Manager GenerateManager(int clubId, int countryId)
        {
            var allManagers = GetAllManagers();
            int newid;
            if (allManagers.Count > 0)
                newid = allManagers.Max(m => m.Id) + 1;
            else
                newid = 0;

            var newManager = new Manager();
            newManager.Id = newid;
            newManager.FirstName = _personNameService.GetRandomFirstName(countryId);
            newManager.LastName = _personNameService.GetRandomLastName(countryId);
            newManager.TrainingDefenseSkill = random.Next(1, 11);
            newManager.TrainingAttackSkill = random.Next(1, 11);
            newManager.TrainingMidfieldSkill = random.Next(1, 11);
            newManager.TrainingGoalkeepingSkill = random.Next(1, 11);
            newManager.Age = random.Next(40, 65);
            newManager.ClubId = clubId;
            newManager.CountryId = countryId;
            CreateManager(newManager);
            return newManager;
        }

        public IList<Manager> GetAllManagers()
        {
            return _managerRepository.Get();
        }

        public Manager GetManager(int id)
        {
            return _managerRepository.Get().FirstOrDefault(m => m.Id == id);

        }

        public Manager UpdateManager(Manager manager)
        {
           var list = GetAllManagers().Where(m => m.Id != manager.Id).ToList();
            list.Add(manager);
            _managerRepository.Create(list);

            return manager;
        }

        //returns new managers
        public IList<Manager> UpdateManagersEndSeason()
        {
            var newManagers = new List<Manager>();
            var allManagers = GetAllManagers();
            var allManagersToLoop = new List<Manager>(allManagers);
            foreach (var manager in allManagersToLoop)
            {
                manager.Age++;
                if (manager.Age > 65)
                {
                    if (random.Next(0, 5) == 0)
                    {
                        var newManager = GenerateManager(manager.ClubId, manager.CountryId);
                        allManagers.Remove(manager);
                        allManagers.Add(newManager);
                        newManagers.Add(newManager);
                    }
                }
            }

            _managerRepository.Create(allManagers);
            
            return newManagers;
        }
    }
}
