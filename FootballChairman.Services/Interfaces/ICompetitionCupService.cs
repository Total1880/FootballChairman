using FootballChairman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services.Interfaces
{
    public interface ICompetitionCupService
    {
        CompetitionCup CreateCompetition(CompetitionCup competition);
        IList<CompetitionCup> GetAllCompetitions();
    }
}
