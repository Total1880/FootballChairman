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
    public class CompetitionService : ICompetitionService
    {
        IRepository<Competition> _competitionService;

        public CompetitionService(IRepository<Competition> competitionRepository)
        {
            _competitionService = competitionRepository;
        }
        public Competition CreateCompetition(Competition competition)
        {
            var list = GetAllCompetitions();
            list.Add(competition);
            _competitionService.Create(list);
            return competition;
        }

        public IList<Competition> GetAllCompetitions()
        {
            return _competitionService.Get();
        }
    }
}
