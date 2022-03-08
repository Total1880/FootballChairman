using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Models
{
    public class ClubPerCompetition
    {
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public int CompetitionId { get; set; }
        public int Points { get; set; }
        public int Win { get; set; }
        public int Draw { get; set; }
        public int Lost { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get => GoalsFor - GoalsAgainst; }
        public bool IsNew { get; set; }
    }
}
