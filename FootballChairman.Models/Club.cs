namespace FootballChairman.Models
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SkillDefense { get; set; }
        public int SkillAttack { get; set; }
        public int SkillMidfield { get; set; }
        public int SkillGoalkeeping { get; set; }
        public int ManagerId { get; set; }
        public int CountryId { get; set; }
        public bool IsPlayer { get; set; }
        public int Reputation { get; set; }
        public float Budget { get; set; }

        public Club()
        {
            Reputation = 5000;
        }
    }
}
