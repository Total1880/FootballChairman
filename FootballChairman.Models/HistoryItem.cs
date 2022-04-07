namespace FootballChairman.Models
{
    public class HistoryItem
    {
        public int Id { get; set; }
        public int ClubId { get; set; }
        public int CompetitionId { get; set; }
        public int Year { get; set; }
        public string ClubName { get; set; }
    }
}
