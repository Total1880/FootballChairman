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
        private IManagerService _managerService;
        private ObservableCollection<Fixture> _fixtures = new ObservableCollection<Fixture>();

        public ObservableCollection<Fixture> Fixtures { get => _fixtures; }

        public FixturesPageViewModel(
            IFixtureService fixtureService,
            ICompetitionService competitionService,
            IClubService clubService,
            IClubPerCompetitionService clubPerCompetitionService, 
            IManagerService managerService)
        {
            _fixtureService = fixtureService;
            _competitionService = competitionService;
            _clubService = clubService;
            _clubPerCompetitionService = clubPerCompetitionService;
            _managerService = managerService;

            CreateCompetition();
            CreateClubs();
            CreateManagers();
            CreateClubsPerCompetitions();
            CreateFixtures();
            _managerService = managerService;
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
            var random = new Random();

            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 0, Name = "Union" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 1, Name = "Club Brugge" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 2, Name = "Antwerp" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 3, Name = "Anderlecht" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 4, Name = "AA Gent" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 5, Name = "KV Mechelen" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 6, Name = "Charleroi" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 7, Name = "RC Genk" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 8, Name = "Cercle Brugge" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 9, Name = "Sint Truiden" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 10, Name = "KV Kortrijk" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 11, Name = "OH Leuven" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 12, Name = "Standard" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 13, Name = "KV Oostende" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 14, Name = "Eupen" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 15, Name = "Zulte Waregem" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 16, Name = "Seraing" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 17, Name = "Beerschot" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 18, Name = "Westerlo" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 19, Name = "RWDM" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 20, Name = "Waasland-Beveren" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 21, Name = "Deinze" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 22, Name = "Moeskroen" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 23, Name = "Lierse Kempenzonen" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 24, Name = "Lommel" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 25, Name = "Virton" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 26, Name = "Dessel" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 27, Name = "Dender" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 28, Name = "FC Luik" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 29, Name = "Patro Eisden" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 30, Name = "Olympic Charleroi" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 31, Name = "Wezet" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 32, Name = "Knokke" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 33, Name = "Heist" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 34, Name = "Francs Borains" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 35, Name = "Sint Eloois-Winkel" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 36, Name = "Thes" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 37, Name = "Tienen" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 38, Name = "Rupel-Boom" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 39, Name = "Mandel United" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 40, Name = "La Louvière-Centre" });
            _clubService.CreateClub(new Club { SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 41, Name = "RC Hoboken" });
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
                if (counter >= competitions[competitionCounter].NumberOfTeams)
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
        private void CreateManagers()
        {
            var clubs = _clubService.GetAllClubs();
            var counter = 0;
            var random = new Random();
            foreach (var club in clubs)
            {
                var newManager = new Manager();
                newManager.Id = counter;
                newManager.FirstName = "first" + counter;
                newManager.LastName = "last" + counter;
                newManager.TrainingDefenseSkill = random.Next(1, 11);
                newManager.TrainingAttackSkill = random.Next(1, 11);
                newManager.TrainingMidfieldSkill = random.Next(1, 11);
                newManager.ClubId = club.Id;
                _managerService.CreateManager(newManager);
                club.ManagerId = counter;
                counter++;
            }
            _clubService.CreateAllClubs(clubs);
        }
    }
}
