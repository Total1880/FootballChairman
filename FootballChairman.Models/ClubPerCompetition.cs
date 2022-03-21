using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Models
{
    public class ClubPerCompetition
    {
        private readonly int _clubId;
        public readonly string _clubName;
        public int ClubId { get => _clubId;}
        public string ClubName { get => _clubName; }
        public int CompetitionId { get; set; }
        public int Points { get; set; }
        public int Win { get; set; }
        public int Draw { get; set; }
        public int Lost { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get => GoalsFor - GoalsAgainst; }
        public bool IsNew { get; set; }
        public string FixtureEliminated { get; set; }

        public ClubPerCompetition(int clubId, string clubName)
        {
            _clubId = clubId;
            _clubName = clubName;
            FixtureEliminated = string.Empty;
        }
    }
}
