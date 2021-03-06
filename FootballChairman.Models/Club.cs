namespace FootballChairman.Models
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ManagerId { get; set; }
        public int CountryId { get; set; }
        public bool IsPlayer { get; set; }
        public int Reputation { get; set; }
        public float Budget { get; set; }
        public float PlayerBudget
        {
            get
            {
                if ((Budget / 12 / 5) <=0)
                {
                    return 0;
                }
                return Budget / 12 / 5; 
            }
        }
        public int YouthFacilities { get; set; }
        public int TrainingFacilities { get; set; }

        public Club()
        {
            Reputation = 5000;
        }
    }
}
