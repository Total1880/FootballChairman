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

        public ObservableCollection<Fixture> Fixtures { get => _fixtures; }

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
            if (_competitionService.GetAllCompetitions().Any())
                return;

            _competition = new Competition { Id = 0, Name = "Eerste Klasse", Skill = 3 };
            _competitionService.CreateCompetition(_competition);
        }

        private void CreateClubs()
        {
            if (_clubService.GetAllClubs().Any())
                return;

            _clubService.CreateClub(new Club { Id = 0, Skill = 2, Name = "Antwerp" });
            _clubService.CreateClub(new Club { Id = 1, Skill = 2, Name = "Anderlecht" });
            _clubService.CreateClub(new Club { Id = 2, Skill = 1, Name = "Club Brugge" });
            _clubService.CreateClub(new Club { Id = 3, Skill = 1, Name = "Union" });
            _clubService.CreateClub(new Club { Id = 4, Skill = 0, Name = "RC Genk" });
            _clubService.CreateClub(new Club { Id = 5, Skill = 0, Name = "AA Gent" });
        }

        private void CreateClubsPerCompetitions()
        {
            if (_clubPerCompetitionService.GetAll().Any())
                return;

            foreach (var club in _clubService.GetAllClubs())
            {
                _clubPerCompetitionService.CreateClubPerCompetition(new ClubPerCompetition { ClubId = club.Id, CompetitionId = _competition.Id, ClubName = club.Name });
            }
        }

        private void CreateFixtures()
        {
            if(_fixtureService.LoadFixtures().Any())
                return ;

            _fixtures = new ObservableCollection<Fixture>(_fixtureService.GenerateFixtures(_clubService.GetAllClubs(), _competition.Id));
        }
    }
}
