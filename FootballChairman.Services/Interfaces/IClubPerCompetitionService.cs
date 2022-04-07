using FootballChairman.Models;

namespace FootballChairman.Services.Interfaces
{
    public interface IClubPerCompetitionService
    {
        ClubPerCompetition CreateClubPerCompetition(ClubPerCompetition clubPerCompetition);
        void CreateInternationalClubPerCompetitions(IList<Club> clubs, int competitionId);
        IList<ClubPerCompetition> GetAll();
        void UpdateData(Game game);
        void UpdateCupData(Game game);
        void ResetData();
        void UpdatePromotionsAndRelegations(IList<ClubPerCompetition> ranking);
    }
}
