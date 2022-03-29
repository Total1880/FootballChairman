using FootballChairman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services.Interfaces
{
    public interface IPlayerService
    {
        Player GenerateRandomPlayer(int clubId);
        Player CreatePlayer(Player player);
        IList<Player> GetPlayers();
        IList<Player> GetPlayersFromClub(int clubId);
    }
}
