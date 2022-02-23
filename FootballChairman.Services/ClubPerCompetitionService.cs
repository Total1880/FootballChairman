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
    public class ClubPerCompetitionService : IClubPerCompetitionService
    {
        IRepository<ClubPerCompetition> _clubPerCompetitionRepository;

        public ClubPerCompetitionService(IRepository<ClubPerCompetition> clubPerCompetitionRepository)
        {
            _clubPerCompetitionRepository = clubPerCompetitionRepository;
        }

        public ClubPerCompetition CreateClubPerCompetition(ClubPerCompetition clubPerCompetition)
        {
            var list = GetAll();
            list.Add(clubPerCompetition);
            _clubPerCompetitionRepository.Create(list);
            return clubPerCompetition;
        }

        public IList<ClubPerCompetition> GetAll()
        {
            return _clubPerCompetitionRepository.Get();
        }
    }
}
