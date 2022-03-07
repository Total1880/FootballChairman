using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Models
{
    public class Competition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Skill { get; set; }
        public int PromotionCompetitionId { get; set; }
        public int RelegationCompetitionId { get; set; }
        public int NumberOfTeams { get; set; }
    }
}
