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
    public class ClubServiceV2 : IClubService
    {
        IRepository<Club> _clubRepository;
        IManagerService _managerService;

        public ClubServiceV2(IRepository<Club> clubRepository, IManagerService managerService)
        {
            _clubRepository = clubRepository;
            _managerService = managerService;
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
            var clubs = GetAllClubs();
            var counter = ranking.Count();
            foreach (var club in ranking)
            {
                var lookUpClub = clubs.FirstOrDefault(c => c.Id == club.ClubId);
                if (lookUpClub == null)
                    continue;

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


                UpdateClub(lookUpClub);
                counter--;
            }
        }

        public void UpdateClubsEndOfSeasonTroughManager()
        {
            var clubs = GetAllClubs();
            var random = new Random();

            foreach (var club in clubs)
            {
                var manager = _managerService.GetManager(club.ManagerId);

                if (club.SkillDefense < manager.TrainingDefenseSkill * 10)
                    club.SkillDefense += random.Next(0, 10);
                else
                    club.SkillDefense -= random.Next(0, 10);

                if (club.SkillMidfield < manager.TrainingMidfieldSkill * 10)
                    club.SkillMidfield += random.Next(0, 10);
                else
                    club.SkillMidfield -= random.Next(0, 10);

                if (club.SkillAttack < manager.TrainingAttackSkill * 10)
                    club.SkillAttack += random.Next(0, 10);
                else
                    club.SkillAttack -= random.Next(0, 10);

                if (club.SkillDefense < 1)
                    club.SkillDefense = 1;

                if (club.SkillMidfield < 1)
                    club.SkillMidfield = 1;

                if (club.SkillAttack < 1)
                    club.SkillAttack = 1;
            }

            _clubRepository.Create(clubs);
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
