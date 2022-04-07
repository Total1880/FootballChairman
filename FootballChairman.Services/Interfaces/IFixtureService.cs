using FootballChairman.Models;

namespace FootballChairman.Services.Interfaces
{
    public interface IFixtureService
    {
        IList<Fixture> SaveFixtures(IList<Fixture> fixtures);
        IList<Fixture> LoadFixtures();
        IList<Fixture> LoadFixturesOfMatchday(int matchday);
        IList<Fixture> GenerateFixtures(IList<Club> teams, int competitionId);
        IList<Fixture> GenerateCupFixtures(IList<Club> teams, CompetitionCup competitionCup);
        void UpdateCupData(Game game, SaveGameData saveGameData);
        Fixture UpdateFixture(Fixture fixture);
    }
}
