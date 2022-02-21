using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Models
{
    public class Fixture
    {
        public int RoundNo { get; set; }
        public int MatchNo { get; set; }
        public int HomeOpponentId { get; set; }
        public int AwayOpponentId { get; set; }
        public string HomeOpponent { get; set; }
        public string AwayOpponent { get; set; }
    }
}
