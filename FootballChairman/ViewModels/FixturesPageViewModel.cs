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

            _competitionService.CreateCompetition(new Competition { Id = 0, Name = "Eerste Klasse", Skill = 15, PromotionCompetitionId = -1, RelegationCompetitionId = 1, NumberOfTeams = 10 });
            _competitionService.CreateCompetition(new Competition { Id = 1, Name = "Tweede Klasse", Skill = 12, PromotionCompetitionId = 0, RelegationCompetitionId = 2, NumberOfTeams = 8 });
            _competitionService.CreateCompetition(new Competition { Id = 2, Name = "Derde Klasse", Skill = 9, PromotionCompetitionId = 1, RelegationCompetitionId = 3, NumberOfTeams = 8 });
            _competitionService.CreateCompetition(new Competition { Id = 3, Name = "Vierde Klasse", Skill = 6, PromotionCompetitionId = 2, RelegationCompetitionId = 4, NumberOfTeams = 8 });
            _competitionService.CreateCompetition(new Competition { Id = 4, Name = "Vijfde Klasse", Skill = 3, PromotionCompetitionId = 3, RelegationCompetitionId = -1, NumberOfTeams = 8 });
        }

        private void CreateClubs()
        {
            if (_clubService.GetAllClubs().Any())
                return;

            _clubService.CreateClub(new Club { Id = 0, Skill = 14, Name = "Union" });
            _clubService.CreateClub(new Club { Id = 1, Skill = 14, Name = "Club Brugge" });
            _clubService.CreateClub(new Club { Id = 2, Skill = 14, Name = "Antwerp" });
            _clubService.CreateClub(new Club { Id = 3, Skill = 13, Name = "Anderlecht" });
            _clubService.CreateClub(new Club { Id = 4, Skill = 13, Name = "AA Gent" });
            _clubService.CreateClub(new Club { Id = 5, Skill = 13, Name = "KV Mechelen" });
            _clubService.CreateClub(new Club { Id = 6, Skill = 12, Name = "Charleroi" });
            _clubService.CreateClub(new Club { Id = 7, Skill = 12, Name = "RC Genk" });
            _clubService.CreateClub(new Club { Id = 8, Skill = 12, Name = "Cercle Brugge" });
            _clubService.CreateClub(new Club { Id = 9, Skill = 11, Name = "Sint Truiden" });
            _clubService.CreateClub(new Club { Id = 10, Skill = 11, Name = "KV Kortrijk" });
            _clubService.CreateClub(new Club { Id = 11, Skill = 11, Name = "OH Leuven" });
            _clubService.CreateClub(new Club { Id = 12, Skill = 10, Name = "Standard" });
            _clubService.CreateClub(new Club { Id = 13, Skill = 10, Name = "KV Oostende" });
            _clubService.CreateClub(new Club { Id = 14, Skill = 10, Name = "Eupen" });
            _clubService.CreateClub(new Club { Id = 15, Skill = 9, Name = "Zulte Waregem" });
            _clubService.CreateClub(new Club { Id = 16, Skill = 9, Name = "Seraing" });
            _clubService.CreateClub(new Club { Id = 17, Skill = 9, Name = "Beerschot" });
            _clubService.CreateClub(new Club { Id = 18, Skill = 8, Name = "Westerlo" });
            _clubService.CreateClub(new Club { Id = 19, Skill = 8, Name = "RWDM" });
            _clubService.CreateClub(new Club { Id = 20, Skill = 8, Name = "Waasland-Beveren" });
            _clubService.CreateClub(new Club { Id = 21, Skill = 7, Name = "Deinze" });
            _clubService.CreateClub(new Club { Id = 22, Skill = 7, Name = "Moeskroen" });
            _clubService.CreateClub(new Club { Id = 23, Skill = 7, Name = "Lierse Kempenzonen" });
            _clubService.CreateClub(new Club { Id = 24, Skill = 6, Name = "Lommel" });
            _clubService.CreateClub(new Club { Id = 25, Skill = 6, Name = "Virton" });
            _clubService.CreateClub(new Club { Id = 26, Skill = 6, Name = "Dessel" });
            _clubService.CreateClub(new Club { Id = 27, Skill = 5, Name = "Dender" });
            _clubService.CreateClub(new Club { Id = 28, Skill = 5, Name = "FC Luik" });
            _clubService.CreateClub(new Club { Id = 29, Skill = 5, Name = "Patro Eisden" });
            _clubService.CreateClub(new Club { Id = 30, Skill = 4, Name = "Olympic Charleroi" });
            _clubService.CreateClub(new Club { Id = 31, Skill = 4, Name = "Wezet" });
            _clubService.CreateClub(new Club { Id = 32, Skill = 4, Name = "Knokke" });
            _clubService.CreateClub(new Club { Id = 33, Skill = 3, Name = "Heist" });
            _clubService.CreateClub(new Club { Id = 34, Skill = 3, Name = "Francs Borains" });
            _clubService.CreateClub(new Club { Id = 35, Skill = 3, Name = "Sint Eloois-Winkel" });
            _clubService.CreateClub(new Club { Id = 36, Skill = 2, Name = "Thes" });
            _clubService.CreateClub(new Club { Id = 37, Skill = 2, Name = "Tienen" });
            _clubService.CreateClub(new Club { Id = 38, Skill = 2, Name = "Rupel-Boom" });
            _clubService.CreateClub(new Club { Id = 39, Skill = 1, Name = "Mandel United" });
            _clubService.CreateClub(new Club { Id = 40, Skill = 1, Name = "La Louvière-Centre" });
            _clubService.CreateClub(new Club { Id = 41, Skill = 0, Name = "RC Hoboken" });
        }

        private void CreateClubsPerCompetitions()
        {
            if (_clubPerCompetitionService.GetAll().Any())
            {
                _clubPerCompetitionService.ResetData();
                return;
            }

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
