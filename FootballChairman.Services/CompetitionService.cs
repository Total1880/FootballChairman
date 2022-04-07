using FootballChairman.Models;
using FootballChairman.Repositories;
using FootballChairman.Services.Interfaces;

namespace FootballChairman.Services
{
    public class CompetitionService : ICompetitionService
    {
        private readonly IRepository<Competition> _competitionService;

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
