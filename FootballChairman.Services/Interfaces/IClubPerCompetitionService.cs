using FootballChairman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services.Interfaces
{
    public interface IClubPerCompetitionService
    {
        ClubPerCompetition CreateClubPerCompetition(ClubPerCompetition clubPerCompetition);
        IList<ClubPerCompetition> GetAll();
        void UpdateData(Game game);
    }
}
