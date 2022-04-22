﻿using FootballChairman.Models;
using FootballChairman.Repositories;
using FootballChairman.Services.Interfaces;
using OlavFramework;

namespace FootballChairman.Services
{
    public class ClubServiceV2 : IClubService
    {
        private readonly IRepository<Club> _clubRepository;
        private readonly IRepository<Player> _playerRepository;
        private readonly IRepository<Manager> _managerRepository;

        public ClubServiceV2(IRepository<Club> clubRepository, IRepository<Player> playerRepository, IRepository<Manager> managerRepository)
        {
            _clubRepository = clubRepository;
            _playerRepository = playerRepository;
            _managerRepository = managerRepository;
        }

        public IList<Club> CreateAllClubs(IList<Club> clubs)
        {
            return _clubRepository.Create(clubs);
        }

        public Club CreateClub(Club club)
        {
            var list = GetAllClubs();
            list.Add(club);
            _clubRepository.Create(list);
            return club;
        }

        public IList<Club> GetAllClubs()
        {
            return _clubRepository.Get();
        }

        public Club GetClub(int id)
        {
            return GetAllClubs().FirstOrDefault(c => c.Id == id);
        }

        public Club UpdateClub(Club club)
        {
            var list = GetAllClubs().Where(c => c.Id != club.Id).ToList();

            list.Add(club);

            return CreateAllClubs(list).FirstOrDefault(c => c.Id == club.Id);
        }

        public void UpdateClubsEndOfSeason(IList<ClubPerCompetition> ranking, int competitionReputation)
        {
            var managers = _managerRepository.Get();
            var clubs = GetAllClubs();
            var counter = ranking.Count();
            foreach (var club in ranking)
            {
                var lookUpClub = clubs.FirstOrDefault(c => c.Id == club.ClubId);
                if (lookUpClub == null)
                {
                    continue;
                }

                //Update reputation
                if (lookUpClub.Reputation < competitionReputation - 100)
                {
                    lookUpClub.Reputation += counter;
                }
                if (lookUpClub.Reputation < competitionReputation - 1000)
                {
                    lookUpClub.Reputation += 100;
                }
                if (lookUpClub.Reputation < competitionReputation - 2000)
                {
                    lookUpClub.Reputation += 500;
                }
                if (lookUpClub.Reputation > competitionReputation + 100)
                {
                    lookUpClub.Reputation -= counter;
                }
                if (lookUpClub.Reputation > competitionReputation + 1000)
                {
                    lookUpClub.Reputation -= 100;
                }
                if (lookUpClub.Reputation > competitionReputation + 2000)
                {
                    lookUpClub.Reputation -= 500;
                }

                //Update budget
                lookUpClub.Budget += lookUpClub.Reputation;

                //Update facilities
                switch (managers.FirstOrDefault(m => m.ClubId == club.ClubId).FacilityUpgradeType)
                {
                    case Models.Enums.FacilityUpgradeType.Youth:
                        if (lookUpClub.YouthFacilities > lookUpClub.TrainingFacilities + 3)
                        {
                            if (Configuration.PriceUpgrade * lookUpClub.TrainingFacilities < lookUpClub.Budget)
                            {
                                lookUpClub.Budget -= Configuration.PriceUpgrade * lookUpClub.TrainingFacilities + 1;
                                lookUpClub.TrainingFacilities++;
                            }
                        }
                        else
                        {
                            if (Configuration.PriceUpgrade * lookUpClub.YouthFacilities < lookUpClub.Budget)
                            {
                                lookUpClub.Budget -= Configuration.PriceUpgrade * lookUpClub.YouthFacilities + 1;
                                lookUpClub.YouthFacilities++;
                            }
                        }
                        break;
                    case Models.Enums.FacilityUpgradeType.FullYouth:
                        if (Configuration.PriceUpgrade * lookUpClub.YouthFacilities < lookUpClub.Budget)
                        {
                            lookUpClub.Budget -= Configuration.PriceUpgrade * lookUpClub.YouthFacilities + 1;
                            lookUpClub.YouthFacilities++;
                        }
                        break;
                    case Models.Enums.FacilityUpgradeType.Training:
                        if (lookUpClub.TrainingFacilities > lookUpClub.YouthFacilities + 3)
                        {
                            if (Configuration.PriceUpgrade * lookUpClub.YouthFacilities < lookUpClub.Budget)
                            {
                                lookUpClub.Budget -= Configuration.PriceUpgrade * lookUpClub.YouthFacilities + 1;
                                lookUpClub.YouthFacilities++;
                            }
                        }
                        else
                        {
                            if (Configuration.PriceUpgrade * lookUpClub.TrainingFacilities < lookUpClub.Budget)
                            {
                                lookUpClub.Budget -= Configuration.PriceUpgrade * lookUpClub.TrainingFacilities + 1;
                                lookUpClub.TrainingFacilities++;
                            }
                        }
                        break;
                    case Models.Enums.FacilityUpgradeType.FullTraining:
                        if (Configuration.PriceUpgrade * lookUpClub.TrainingFacilities < lookUpClub.Budget)
                        {
                            lookUpClub.Budget -= Configuration.PriceUpgrade * lookUpClub.TrainingFacilities + 1;
                            lookUpClub.TrainingFacilities++;
                        }
                        break;

                    default:
                        break;
                }

                //Downgrade facilities
                if (lookUpClub.TrainingFacilities > 0 && (Configuration.CeilingFacilities - lookUpClub.TrainingFacilities < 0 || new Random().Next(0, Configuration.CeilingFacilities - lookUpClub.TrainingFacilities) == 0))
                {
                    lookUpClub.TrainingFacilities--;
                }
                if (lookUpClub.YouthFacilities > 0 && (Configuration.CeilingFacilities - lookUpClub.YouthFacilities < 0 || new Random().Next(0, Configuration.CeilingFacilities - lookUpClub.YouthFacilities) == 0))
                {
                    lookUpClub.YouthFacilities--;
                }

                UpdateClub(lookUpClub);
                counter--;
            }
        }

        public void UpdateClubsWithNewManagers(IList<Manager> newManagers)
        {
            if (newManagers.Count > 0)
            {
                foreach (var manager in newManagers)
                {
                    var club = GetClub(manager.ClubId);
                    club.ManagerId = manager.Id;
                    UpdateClub(club);
                }
            }
        }
    }
}
