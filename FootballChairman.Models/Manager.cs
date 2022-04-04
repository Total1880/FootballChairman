using FootballChairman.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Models
{
    public class Manager : Person
    {
        public int TrainingAttackSkill { get; set; }
        public int TrainingDefenseSkill { get; set; }
        public int TrainingMidfieldSkill { get; set; }
        public int TrainingGoalkeepingSkill { get; set; }
        public ManagerType ManagerType { get; set; }
    }
}
