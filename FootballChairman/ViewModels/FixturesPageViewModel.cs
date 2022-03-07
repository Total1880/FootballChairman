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

            _competitionService.CreateCompetition(new Competition { Id = 0, Name = "Eerste Klasse", Skill = 9, PromotionCompetitionId = -1, RelegationCompetitionId = 1, NumberOfTeams = 10 });
            _competitionService.CreateCompetition(new Competition { Id = 1, Name = "Tweede Klasse", Skill = 6, PromotionCompetitionId = 0, RelegationCompetitionId = 2, NumberOfTeams = 8 });
            _competitionService.CreateCompetition(new Competition { Id = 2, Name = "Derde Klasse", Skill = 3, PromotionCompetitionId = 1, RelegationCompetitionId = -1, NumberOfTeams = 8 });
        }

        private void CreateClubs()
        {
            if (_clubService.GetAllClubs().Any())
                return;

            _clubService.CreateClub(new Club { Id = 0, Skill = 5, Name = "Union" });
            _clubService.CreateClub(new Club { Id = 1, Skill = 5, Name = "Club Brugge" });
            _clubService.CreateClub(new Club { Id = 2, Skill = 5, Name = "Antwerp" });
            _clubService.CreateClub(new Club { Id = 3, Skill = 4, Name = "Anderlecht" });
            _clubService.CreateClub(new Club { Id = 4, Skill = 4, Name = "AA Gent" });
            _clubService.CreateClub(new Club { Id = 5, Skill = 4, Name = "KV Mechelen" });
            _clubService.CreateClub(new Club { Id = 6, Skill = 3, Name = "Charleroi" });
            _clubService.CreateClub(new Club { Id = 7, Skill = 3, Name = "RC Genk" });
            _clubService.CreateClub(new Club { Id = 8, Skill = 3, Name = "Cercle Brugge" });
            _clubService.CreateClub(new Club { Id = 9, Skill = 2, Name = "Sint Truiden" });
            _clubService.CreateClub(new Club { Id = 10, Skill = 2, Name = "KV Kortrijk" });
            _clubService.CreateClub(new Club { Id = 11, Skill = 2, Name = "OH Leuven" });
            _clubService.CreateClub(new Club { Id = 12, Skill = 1, Name = "Standard" });
            _clubService.CreateClub(new Club { Id = 13, Skill = 1, Name = "KV Oostende" });
            _clubService.CreateClub(new Club { Id = 14, Skill = 1, Name = "Eupen" });
            _clubService.CreateClub(new Club { Id = 15, Skill = 0, Name = "Zulte Waregem" });
            _clubService.CreateClub(new Club { Id = 16, Skill = 0, Name = "Seraing" });
            _clubService.CreateClub(new Club { Id = 17, Skill = 0, Name = "Beerschot" });
            _clubService.CreateClub(new Club { Id = 18, Skill = 0, Name = "Westerlo" });
            _clubService.CreateClub(new Club { Id = 19, Skill = 0, Name = "RWDM" });
            _clubService.CreateClub(new Club { Id = 20, Skill = 0, Name = "Waasland-Beveren" });
            _clubService.CreateClub(new Club { Id = 21, Skill = 0, Name = "Deinze" });
            _clubService.CreateClub(new Club { Id = 22, Skill = 0, Name = "Moeskroen" });
            _clubService.CreateClub(new Club { Id = 23, Skill = 0, Name = "Lierse Kempenzonen" });
            _clubService.CreateClub(new Club { Id = 24, Skill = 0, Name = "Lommel" });
            _clubService.CreateClub(new Club { Id = 25, Skill = 0, Name = "Virton" });
        }

        private void CreateClubsPerCompetitions()
        {
            if (_clubPerCompetitionService.GetAll().Any())
                return;

            var competitions = _competitionService.GetAllCompetitions();
            int counter = 0;
            int competitionCounter = 0;

            foreach (var club in _clubService.GetAllClubs())
            {
                _clubPerCompetitionService.CreateClubPerCompetition(new ClubPerCompetition { ClubId = club.Id, CompetitionId = competitions[competitionCounter].Id, ClubName = club.Name });

                counter++;
                if (counter >= competitions[competitionCounter].NumberOfTeams )
                {
                    counter = 0;
                    competitionCounter++;
                }
            }
        }

        private void CreateFixtures()
        {
            if (_fixtureService.LoadFixtures().Any())
                return;

            var clubs = _clubService.GetAllClubs();
            var competitions = _competitionService.GetAllCompetitions();
            var clubsPerCompetition = _clubPerCompetitionService.GetAll();

            foreach (var competition in competitions)
            {
                var listOfClubs = new List<Club>();

                foreach (var club in clubsPerCompetition.Where(c => c.CompetitionId == competition.Id))
                {
                    listOfClubs.Add(clubs.FirstOrDefault(c => c.Id == club.ClubId));
                }

                _fixtures = new ObservableCollection<Fixture>(_fixtureService.GenerateFixtures(listOfClubs, competition.Id));
            }
        }
    }
}
