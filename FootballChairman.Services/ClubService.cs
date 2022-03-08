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
    public class ClubService : IClubService
    {
        IRepository<Club> _clubRepository;
        ICompetitionService _competitionService;

        public ClubService(IRepository<Club> clubRepository, ICompetitionService competitionService)
        {
            _clubRepository = clubRepository;
            _competitionService = competitionService;
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
        }
    }
}
