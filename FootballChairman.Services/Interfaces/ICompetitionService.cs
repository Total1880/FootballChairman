using FootballChairman.Models;

namespace FootballChairman.Services.Interfaces
{
    public interface ICompetitionService
    {
        Competition CreateCompetition(Competition competition);
        IList<Competition> GetAllCompetitions();
    }
}
