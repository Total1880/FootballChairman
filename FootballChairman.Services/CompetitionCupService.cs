using FootballChairman.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services
{
    public class CompetitionCupService : ICompetitionCupService
    {
        public CompetitionCupService()
        {
            CalculateRounds();
        }
        private void CalculateRounds()
        {
            var testNumberOfClubs = 18;
            var test = Math.Sqrt(testNumberOfClubs);
            var testPow = Math.Pow(2, testNumberOfClubs);

            testNumberOfClubs = 10;
            var test2 = Math.Sqrt(testNumberOfClubs);
            var testPow2 = Math.Pow(2, testNumberOfClubs);

            testNumberOfClubs = 36;
            var test3 = Math.Sqrt(testNumberOfClubs);
            var testPow3 = Math.Pow(2, testNumberOfClubs);

            testNumberOfClubs = 8;
            var test4 = Math.Sqrt(testNumberOfClubs);
            var testPow4 = Math.Pow(2, testNumberOfClubs);

            testNumberOfClubs = 21;
            var test5 = Math.Sqrt(testNumberOfClubs);
            var testPow5 = Math.Pow(2, testNumberOfClubs);

        }
    }
}
