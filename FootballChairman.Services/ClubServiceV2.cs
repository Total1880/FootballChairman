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
        ICompetitionService _competitionService;
        IManagerService _managerService;

        public ClubServiceV2(IRepository<Club> clubRepository, ICompetitionService competitionService, IManagerService managerService)
        {
            _clubRepository = clubRepository;
            _competitionService = competitionService;
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

        public void UpdateClubsEndOfSeason(IList<ClubPerCompetition> ranking)
        {
            //var clubs = GetAllClubs();

            //// update first club
            //var firstClub = clubs.FirstOrDefault(c => c.Id == ranking[0].ClubId);
            //if (firstClub.Skill < _competitionService.GetAllCompetitions().FirstOrDefault(com => com.Id == ranking[0].CompetitionId).Skill)
            //    firstClub.Skill++;

            //// update last club
            //var lastClub = clubs.FirstOrDefault(c => c.Id == ranking[ranking.Count - 1].ClubId);
            //if(lastClub.Skill >0)
            //    lastClub.Skill--;

            //var random = new Random();
            //int counter = 0;
            //int backCounter = ranking.Count + 1;
            //foreach (var clubPerCompetition in ranking)
            //{
            //    counter++;
            //    backCounter--;

            //    if (counter != 1 || counter == ranking.Count)
            //    {
            //        if (random.Next(counter) == 0)
            //        {
            //            if (clubs.FirstOrDefault(c => c.Id == clubPerCompetition.ClubId).Skill < _competitionService.GetAllCompetitions().FirstOrDefault(com => com.Id == ranking[0].CompetitionId).Skill)
            //                clubs.FirstOrDefault(c => c.Id == clubPerCompetition.ClubId).Skill++;
            //        }
            //        else if (random.Next(backCounter) == 0)
            //        {
            //            if (clubs.FirstOrDefault(c => c.Id == clubPerCompetition.ClubId).Skill > 0)
            //                clubs.FirstOrDefault(c => c.Id == clubPerCompetition.ClubId).Skill--;
            //        }
            //    }
            //}

            //_clubRepository.Create(clubs);
            throw new NotImplementedException();

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
    }
}
