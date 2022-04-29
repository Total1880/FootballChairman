using FootballChairman.Models;

namespace FootballChairman.Services.Interfaces
{
    public interface IClubService
    {
        Club CreateClub(Club club);
        Club GetClub(int id);
        IList<Club> GetAllClubs();
        IList<Club> CreateAllClubs(IList<Club> clubs);
        void UpdateClubsEndOfSeason(IList<ClubPerCompetition> ranking, int competitionReputation);
        void UpdateClubsWithNewManagers(IList<Manager> newManagers);
        Club UpdateClub(Club club);
        void UpdateClubFinanceTransfers(IList<Transfer> newTransfers);

    }
}
