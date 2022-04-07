namespace FootballChairman.Models
{
    public class Competition : CompetitionBase
    {

        public int PromotionCompetitionId { get; set; }
        public int RelegationCompetitionId { get; set; }
        public int NumberOfTeams { get; set; }


    }
}