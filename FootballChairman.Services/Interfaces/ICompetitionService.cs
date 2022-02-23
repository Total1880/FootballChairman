using FootballChairman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services.Interfaces
{
    public interface ICompetitionService
    {
        Competition CreateCompetition(Competition competition);
        IList<Competition> GetAllCompetitions();
    }
}
