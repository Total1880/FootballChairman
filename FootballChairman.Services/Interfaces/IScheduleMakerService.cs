using FootballChairman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services.Interfaces
{
    public interface IScheduleMakerService
    {
        IList<Fixture> Generate(IList<Club> teams, int competitionId);
        IList<Fixture> GenerateCup(IList<Club> teams, CompetitionCup competitionCup);
    }
}
