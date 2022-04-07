using FootballChairman.Models.Enums;

namespace FootballChairman.Models
{
    public abstract class CompetitionBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Reputation { get; set; }
        public CompetitionType CompetitionType { get; set; }
        public int CountryId { get; set; }
    }
}
