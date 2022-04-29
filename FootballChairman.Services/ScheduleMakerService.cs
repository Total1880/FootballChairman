using FootballChairman.Models;
using FootballChairman.Services.Interfaces;

namespace FootballChairman.Services
{
    public class ScheduleMakerService : IScheduleMakerService
    {
        private IList<Club> _teams;
        private int _roundCount;
        private int _matchesPerRoundCount;
        private bool _alternate = false;
        private IList<int> _offsetList;
        private readonly Random rng = new Random();

        public ScheduleMakerService()
        {
            _teams = new List<Club>();
            _offsetList = new List<int>();
        }

        public IList<Fixture> Generate(IList<Club> teams, int competitionId)
        {
            _teams = teams;
            Shuffle(_teams);
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

        public IList<Fixture> GenerateCup(IList<Club> teams, CompetitionCup competitionCup)
        {
            if (teams == null || teams.Count < 2)
            {
                return new List<Fixture>();
            }

            int totalNumberOfTeams = teams.Count;
            var fixtures = new List<Fixture>();
            var cupround = 0;
            int counter = 0;

            if (totalNumberOfTeams % 2 != 0)
            {
                teams.Add(new Club { Id = -1, Name = "bye" });
            }


            var extrateams = 0;
            var nextRoundTeams = totalNumberOfTeams;

            while (IsInBinarySequence(nextRoundTeams) == -1)
            {
                extrateams++;
                nextRoundTeams--;
            }

            competitionCup.Rounds = IsInBinarySequence(nextRoundTeams);

            //Create extra round
            if (extrateams > 0)
            {
                var extraRoundTeams = teams.OrderBy(t => t.Reputation).Take(extrateams * 2).ToList();
                teams = teams.OrderBy(t => t.Reputation).Skip(extrateams * 2).ToList();
                Shuffle(extraRoundTeams);
                for (int i = 0; i < extrateams * 2; i += 2)
                {
                    Fixture fixture = new Fixture();
                    counter++;

                    fixture.AwayOpponentId = extraRoundTeams[i].Id;
                    fixture.HomeOpponentId = extraRoundTeams[i + 1].Id;
                    fixture.RoundNo = cupround;
                    fixture.MatchNo = counter;
                    fixture.AwayOpponent = extraRoundTeams[i].Name;
                    fixture.HomeOpponent = extraRoundTeams[i + 1].Name;
                    fixture.CompetitionId = competitionCup.Id;

                    fixtures.Add(fixture);
                }
                cupround++;
                counter = 0;
            }

            Shuffle(teams);

            //Create first round
            for (int i = 0; i < nextRoundTeams /** 2 - extrateams*/; i += 2)
            {
                Fixture fixture = new Fixture();

                fixture.RoundNo = cupround;
                fixture.MatchNo = counter;
                fixture.CompetitionId = competitionCup.Id;

                if (extrateams > i)
                {
                    fixture.CupPreviousFixtureHomeTeam = fixtures[i].IdString;
                    fixture.CupPreviousFixtureAwayTeam = fixtures[i + 1].IdString;
                }
                else
                {
                    fixture.AwayOpponentId = teams[i - extrateams].Id;
                    fixture.HomeOpponentId = teams[i - extrateams + 1].Id;
                    fixture.AwayOpponent = teams[i - extrateams].Name;
                    fixture.HomeOpponent = teams[i - extrateams + 1].Name;
                }
                fixtures.Add(fixture);
                counter++;
            }
            cupround++;

            while (cupround < competitionCup.Rounds)
            {
                var roundMatches = fixtures.Where(f => f.RoundNo == cupround - 1).Count();
                var newFixtures = new List<Fixture>();
                counter = 0;

                for (int i = 0; i < roundMatches; i += 2)
                {
                    Fixture fixture = new Fixture();

                    fixture.RoundNo = cupround;
                    fixture.MatchNo = counter;
                    fixture.CompetitionId = competitionCup.Id;
                    fixture.CupPreviousFixtureHomeTeam = fixtures[fixtures.Count() - i - 1].IdString;
                    fixture.CupPreviousFixtureAwayTeam = fixtures[fixtures.Count() - i - 2].IdString;
                    newFixtures.Add(fixture);
                    counter++;
                }
                fixtures.AddRange(newFixtures);

                cupround++;
            }

            return fixtures;
        }

        private int IsInBinarySequence(int number)
        {
            var numbertocheck = 1;
            var counter = 0;
            do
            {
                counter++;
                if (number == numbertocheck)
                {
                    return counter;
                }

                numbertocheck *= 2;
            } while (numbertocheck <= number);

            return -1;
        }
        private void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
