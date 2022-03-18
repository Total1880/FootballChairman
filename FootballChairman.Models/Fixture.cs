using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Models
{
    public class Fixture
    {
        public Fixture()
        {
            HomeOpponent = string.Empty;
            AwayOpponent = string.Empty;
            CupPreviousFixtureHomeTeam = string.Empty;
            CupPreviousFixtureAwayTeam = string.Empty;
        }

        public int RoundNo { get; set; }
        public int MatchNo { get; set; }
        public int HomeOpponentId { get; set; }
        public int AwayOpponentId { get; set; }
        public string HomeOpponent { get; set; }
        public string AwayOpponent { get; set; }
        public int CompetitionId { get; set; }
        public string CupPreviousFixtureHomeTeam { get; set; }
        public string CupPreviousFixtureAwayTeam { get; set; }
        public string IdString { get => CompetitionId + ";" + RoundNo + ";" + MatchNo; }
    }
}
