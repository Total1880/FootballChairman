using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Models
{
    public class Game
    {
        public Fixture Fixture { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
    }
}
