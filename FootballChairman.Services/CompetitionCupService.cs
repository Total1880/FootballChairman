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
    public class CompetitionCupService : ICompetitionCupService
    {
        IRepository<CompetitionCup> _competitionCupRepository;
        public CompetitionCupService(IRepository<CompetitionCup> competitionCupRepository)
        {
            _competitionCupRepository = competitionCupRepository;
        }

        public CompetitionCup CreateCompetition(CompetitionCup competition)
        {
            var list = GetAllCompetitions();
            list.Add(competition);
            _competitionCupRepository.Create(list);
            return competition;
        }

        public IList<CompetitionCup> GetAllCompetitions()
        {
            return _competitionCupRepository.Get();
        }
    }
}
