using FootballChairman.Models;
using FootballChairman.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services
{
    public class GameService : IGameService
    {
        public Game PlayGame(Fixture fixture)
        {
            var game = new Game();
            game.Fixture = fixture;
            game.HomeScore = RandomInt(0, 6);
            game.AwayScore = RandomInt(0, 6);

            return game;
        }

        static int RandomInt(int min, int max)
        {
            Random random = new Random();
            int val = random.Next(min, max);
            return val;
        }
    }
}
