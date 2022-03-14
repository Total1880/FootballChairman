using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Models
{
    public class HistoryItem
    {
        public int Id { get; set; }
        public int ClubId { get; set; }
        public int CompetitionId { get; set; }
        public int Year { get; set; }
    }
}
