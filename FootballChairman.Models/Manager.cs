using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int TrainingAttackSkill { get; set; }
        public int TrainingDefenseSkill { get; set; }
        public int TrainingMidfieldSkill { get; set; }
        public int ClubId { get; set; }
    }
}
