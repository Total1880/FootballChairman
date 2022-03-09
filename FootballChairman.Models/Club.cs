using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Models
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SkillDefense { get; set; }
        public int SkillAttack { get; set; }
        public int SkillMidfield { get; set; }
        public int ManagerId { get; set; }
        public bool IsPlayer { get; set; }
    }
}
