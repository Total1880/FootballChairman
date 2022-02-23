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
            var clubs = GetAllClubs();

            var firstClub = clubs.FirstOrDefault(c => c.Id == ranking[0].ClubId);
            if (firstClub.Skill < _competitionService.GetAllCompetitions().FirstOrDefault(com => com.Id == ranking[0].CompetitionId).Skill)
                firstClub.Skill++;

            var lastClub = clubs.FirstOrDefault(c => c.Id == ranking[ranking.Count - 1].ClubId);
            if(lastClub.Skill >0)
                lastClub.Skill--;

            _clubRepository.Create(clubs);
        }
    }
}
