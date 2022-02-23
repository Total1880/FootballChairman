using FootballChairman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services.Interfaces
{
    public interface IClubService
    {
        Club CreateClub(Club club);
        Club GetClub(int id);
        IList<Club> GetAllClubs();
        void UpdateClubsEndOfSeason(IList<ClubPerCompetition> ranking);
    }
}
