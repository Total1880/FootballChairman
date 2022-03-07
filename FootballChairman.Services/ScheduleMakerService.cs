using FootballChairman.Models;
using FootballChairman.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services
{
    public class ScheduleMakerService : IScheduleMakerService
    {
        private IList<Club> _teams;
        private int _roundCount;
        private int _matchesPerRoundCount;
        private bool _alternate = false;
        private IList<int> _offsetList;

        public ScheduleMakerService()
        {
            _teams = new List<Club>();
            _offsetList = new List<int>();
        }

        public IList<Fixture> Generate(IList<Club> teams, int competitionId)
        {
            _teams = teams;
            _roundCount = _teams.Count - 1;
            _matchesPerRoundCount = teams.Count / 2;

            var firstHalfSeasonFixtures = GenerateFixtures(0, competitionId);
            var secondHalfSeasonFixtures = GenerateFixtures(_teams.Count - 1, competitionId);

            var list = firstHalfSeasonFixtures;
            list = list.Concat(secondHalfSeasonFixtures).ToList();

            return list;
        }

        private IList<Fixture> GenerateFixtures(int roundNoOffset, int competitionId)
        {
            IList<Fixture> fixtures = new List<Fixture>();
            _offsetList = GenerateOffsetArray(_teams.Count);

            for (int roundNo = 1; roundNo <= _roundCount; roundNo++)
            {
                _alternate = !_alternate;

                IList<int> homes = getHomes(roundNo);
                IList<int> aways = getAways(roundNo);

                for (int matchIndex = 0; matchIndex < _matchesPerRoundCount; matchIndex++)
                {
                    if (_alternate)
                    {
                        fixtures.Add(new Fixture
                        {
                            RoundNo = roundNo + roundNoOffset,
                            MatchNo = matchIndex,
                            HomeOpponentId = _teams[homes[matchIndex]].Id,
                            AwayOpponentId = _teams[aways[matchIndex]].Id,
                            HomeOpponent = _teams[homes[matchIndex]].Name,
                            AwayOpponent = _teams[aways[matchIndex]].Name,
                            CompetitionId = competitionId
                        });
                    }
                    else
                    {
                        fixtures.Add(new Fixture
                        {
                            RoundNo = roundNo + roundNoOffset,
                            MatchNo = matchIndex,
                            HomeOpponentId = _teams[aways[matchIndex]].Id,
                            AwayOpponentId = _teams[homes[matchIndex]].Id,
                            HomeOpponent = _teams[aways[matchIndex]].Name,
                            AwayOpponent = _teams[homes[matchIndex]].Name,
                            CompetitionId = competitionId
                        });
                    }

                    if (homes[matchIndex] == aways[matchIndex])
                    {
                        throw new Exception("Teams cannot play themselves");
                    }
                }
            }
            return fixtures;
        }

        private IList<int> getHomes(int roundNo)
        {
            var offset = _teams.Count - roundNo;
            var array = _offsetList.ToArray();
            var homes = new ArraySegment<int>(array, offset, _matchesPerRoundCount - 1);

            var output = homes.ToList();
            output.Add(0);
            return output;
        }

        private IList<int> getAways(int roundNo)
        {
            var offset = _teams.Count - roundNo + _matchesPerRoundCount - 1;
            var array = _offsetList.ToArray();
            var aways = new ArraySegment<int>(array, offset, _matchesPerRoundCount);
            var output = aways.ToArray();
            Array.Reverse(output);

            return output;
        }

        private IList<int> GenerateOffsetArray(int count)
        {
            var offsetArray = new List<int>();

            for (int i = 1; i < count; i++)
            {
                offsetArray.Add(i);
            }

            offsetArray = offsetArray.Concat(offsetArray).ToList();
            offsetArray = offsetArray.Concat(offsetArray).ToList();
            return offsetArray;
        }
    }
}
