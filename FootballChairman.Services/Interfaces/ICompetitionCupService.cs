using FootballChairman.Models;

namespace FootballChairman.Services.Interfaces
{
    public interface ICompetitionCupService
    {
        CompetitionCup CreateCompetition(CompetitionCup competition);
        IList<CompetitionCup> GetAllCompetitions();
    }
}
