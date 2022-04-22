using FootballChairman.Models.Enums;

namespace FootballChairman.Models
{
    public class Manager : Person
    {
        public int TrainingAttackSkill { get; set; }
        public int TrainingDefenseSkill { get; set; }
        public int TrainingMidfieldSkill { get; set; }
        public int TrainingGoalkeepingSkill { get; set; }
        public ManagerType ManagerType { get; set; }
        public FacilityUpgradeType FacilityUpgradeType { get; set; }
    }
}
