using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Models
{
    public class Tactic
    {
        public int ClubId { get; set; }
        public IList<Player> Defenders { get; set; }
        public IList<Player> Midfielders { get; set; }
        public IList<Player> Attackers { get; set; }
        public Player Goalkeeper { get; set; }
    }
}
