using FootballChairman.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Models
{
    public abstract class CompetitionBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Reputation { get; set; }
        public CompetitionType CompetitionType { get; set; }
        public int CountryId { get; set; }
    }
}
