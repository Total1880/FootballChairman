using FootballChairman.Models;

namespace FootballChairman.Services.Interfaces
{
    public interface IGameService
    {
        Game PlayGame(Fixture fixture, bool suddendeath, Tactic homeTactic, Tactic awayTactic);
        IList<Game> GetGames();
        void CleanGames();
    }
}
