using FootballChairman.Models;
using FootballChairman.Repositories;
using FootballChairman.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services
{
    public class FixtureService : IFixtureService
    {
        private IRepository<Fixture> _fixtureRepository;
        private IScheduleMakerService _scheduleMakerService;

        public FixtureService(IRepository<Fixture> fixtureRepository, IScheduleMakerService scheduleMakerService)
        {
            _fixtureRepository = fixtureRepository;
            _scheduleMakerService = scheduleMakerService;
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
    }
}
