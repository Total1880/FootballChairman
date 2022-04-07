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
        public IList<int> DefendersId { get; set; }
        public IList<Player> Midfielders { get; set; }
        public IList<int> MidfieldersId { get; set; }
        public IList<Player> Attackers { get; set; }
        public IList<int> AttackersId { get; set; }
        public Player Goalkeeper { get; set; }
        public int GoalkeeperId { get; set;}
    }
}
