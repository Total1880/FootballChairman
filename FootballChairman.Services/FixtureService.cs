using FootballChairman.Models;
using FootballChairman.Repositories;
using FootballChairman.Services.Interfaces;

namespace FootballChairman.Services
{
    public class FixtureService : IFixtureService
    {
        private readonly IRepository<Fixture> _fixtureRepository;
        private readonly IScheduleMakerService _scheduleMakerService;
        private readonly IHistoryItemService _historyItemService;
        private readonly IClubPerCompetitionService _clubPerCompetitionService;
        private readonly IClubService _clubService;

        public FixtureService(IRepository<Fixture> fixtureRepository, IScheduleMakerService scheduleMakerService, IHistoryItemService historyItemService, IClubPerCompetitionService clubPerCompetitionService, IClubService clubService)
        {
            _fixtureRepository = fixtureRepository;
            _scheduleMakerService = scheduleMakerService;
            _historyItemService = historyItemService;
            _clubPerCompetitionService = clubPerCompetitionService;
            _clubService = clubService;
        }

        public IList<Fixture> LoadFixtures()
        {
            return _fixtureRepository.Get();
        }

        public IList<Fixture> SaveFixtures(IList<Fixture> fixtures)
        {
            return _fixtureRepository.Create(fixtures);
        }

        public IList<Fixture> GenerateFixtures(IList<Club> teams, int competitionId)
        {
            var fixtures = LoadFixtures().Where(f => f.CompetitionId != competitionId).ToList();
            var newFixtures = _scheduleMakerService.Generate(teams, competitionId);

            fixtures.AddRange(newFixtures);
            return SaveFixtures(fixtures);
        }

        public IList<Fixture> LoadFixturesOfMatchday(int matchday)
        {
            return LoadFixtures().Where(f => f.RoundNo == matchday).ToList();
        }

        public IList<Fixture> GenerateCupFixtures(IList<Club> teams, CompetitionCup competition)
        {
            var fixtures = LoadFixtures().Where(f => f.CompetitionId != competition.Id).ToList();
            var newFixtures = _scheduleMakerService.GenerateCup(teams, competition);

            fixtures.AddRange(newFixtures);
            return SaveFixtures(fixtures);
        }

        public void UpdateCupData(Game game, SaveGameData saveGameData)
        {
            var homeFixture = LoadFixturesOfMatchday(game.Fixture.RoundNo + 1).FirstOrDefault(f => f.CupPreviousFixtureHomeTeam == game.Fixture.IdString);
            var awayFixture = LoadFixturesOfMatchday(game.Fixture.RoundNo + 1).FirstOrDefault(f => f.CupPreviousFixtureAwayTeam == game.Fixture.IdString);

            if (homeFixture != null && awayFixture != null)
            {
                throw new Exception("multiple cupfixtures found ...");
            }

            var losingclub = _clubPerCompetitionService.GetAll().FirstOrDefault(cpc => cpc.FixtureEliminated == game.Fixture.IdString).ClubId;
            var winningclub = losingclub == game.Fixture.AwayOpponentId ? game.Fixture.HomeOpponentId : game.Fixture.AwayOpponentId;

            if (homeFixture == null && awayFixture == null)
            {
                _historyItemService.CreateHistoryItem(new HistoryItem { ClubId = winningclub, CompetitionId = game.Fixture.CompetitionId, Year = saveGameData.Year });
                return;
            }

            if (homeFixture != null)
            {
                homeFixture.HomeOpponentId = winningclub;
                UpdateFixture(homeFixture);
                return;
            }
            if (awayFixture != null)
            {
                awayFixture.AwayOpponentId = winningclub;
                UpdateFixture(awayFixture);
                return;
            }

            throw new Exception("no fixture updated...");
        }
        public Fixture UpdateFixture(Fixture fixture)
        {
            if (fixture.HomeOpponentId >= 0 && fixture.HomeOpponent.Length <= 0)
            {
                fixture.HomeOpponent = _clubService.GetClub(fixture.HomeOpponentId).Name;
            }

            if (fixture.AwayOpponentId >= 0 && fixture.AwayOpponent.Length <= 0)
            {
                fixture.AwayOpponent = _clubService.GetClub(fixture.AwayOpponentId).Name;
            }

            var fixtures = LoadFixtures();
            fixtures.Remove(fixtures.FirstOrDefault(f => f.IdString == fixture.IdString));
            fixtures.Add(fixture);
            SaveFixtures(fixtures);

            return fixture;
        }
       


    }
}
