using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Models
{
    public class Game
    {
        private int _homeScore;
        private int _awayScore;
        public Fixture Fixture { get; set; }
        public int HomeScore { get => _homeScore; set { _homeScore = value; if (_homeScore < 0) { _homeScore = 0; }; } }
        public int AwayScore { get => _awayScore; set { _awayScore = value; if (_awayScore < 0) { _awayScore = 0; }; } }
    }
}
