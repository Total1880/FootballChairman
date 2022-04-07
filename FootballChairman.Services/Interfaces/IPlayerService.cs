using FootballChairman.Models;

namespace FootballChairman.Services.Interfaces
{
    public interface IPlayerService
    {
        Player GenerateRandomPlayer(int clubId, int countryId);
        Player GenerateYouthPlayer(int clubId, int countryId);
        Player CreatePlayer(Player player);
        IList<Player> GetPlayers();
        IList<Player> GetPlayersFromClub(int clubId);
        IList<Player> UpdatePlayersEndOfSeason();
        void CheckIfClubHasEnoughPlayers(int clubId);
    }
}
