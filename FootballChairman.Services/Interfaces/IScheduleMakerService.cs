using FootballChairman.Models;

namespace FootballChairman.Services.Interfaces
{
    public interface IScheduleMakerService
    {
        IList<Fixture> Generate(IList<Club> teams, int competitionId);
        IList<Fixture> GenerateCup(IList<Club> teams, CompetitionCup competitionCup);
    }
}
