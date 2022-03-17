using FootballChairman.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Models
{
    public class Competition : CompetitionBase
    {

        public int PromotionCompetitionId { get; set; }
        public int RelegationCompetitionId { get; set; }
        public int NumberOfTeams { get; set; }
        

    }
}