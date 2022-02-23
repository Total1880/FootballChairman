using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballChairman.Models;
using FootballChairman.Services.Interfaces;
using GalaSoft.MvvmLight;

namespace FootballChairman.ViewModels
{
    public class FixturesPageViewModel : ViewModelBase
    {
        private IFixtureService _fixtureService;
        private ICompetitionService _competitionService;
        private IClubService _clubService;
        private IClubPerCompetitionService _clubPerCompetitionService;
        private ObservableCollection<Fixture> _fixtures = new ObservableCollection<Fixture>();
        private Competition _competition;

        public ObservableCollection<Fixture>Fixtures{ get => _fixtures; }

        public FixturesPageViewModel(IFixtureService fixtureService, ICompetitionService competitionService, IClubService clubService, IClubPerCompetitionService clubPerCompetitionService)
        {
            _fixtureService = fixtureService;
            _competitionService = competitionService;
            _clubService = clubService;
            _clubPerCompetitionService = clubPerCompetitionService;

            CreateCompetition();
            CreateClubs();
            CreateClubsPerCompetitions();
            CreateFixtures();
        }

        private void CreateCompetition()
        {
            _competition = new Competition { Id = 0, Name = "Eerste Klasse" };
            _competitionService.CreateCompetition(_competition);
        }

        private void CreateClubs()
        {
            _clubService.CreateClub(new Club { Id = 0, Name = "Antwerp" });
            _clubService.CreateClub(new Club { Id = 1, Name = "Anderlecht" });
            _clubService.CreateClub(new Club { Id = 2, Name = "Club Brugge" });
            _clubService.CreateClub(new Club { Id = 3, Name = "Union" });
            _clubService.CreateClub(new Club { Id = 4, Name = "RC Genk" });
            _clubService.CreateClub(new Club { Id = 5, Name = "AA Gent" });
        }

        private void CreateClubsPerCompetitions()
        {
            foreach (var club in _clubService.GetAllClubs())
            {
                _clubPerCompetitionService.CreateClubPerCompetition(new ClubPerCompetition { ClubId = club.Id, CompetitionId = _competition.Id });
            }
        }

        private void CreateFixtures()
        {
            _fixtures = new ObservableCollection<Fixture>(_fixtureService.GenerateFixtures(_clubService.GetAllClubs(), _competition.Id));
        }
    }
}
