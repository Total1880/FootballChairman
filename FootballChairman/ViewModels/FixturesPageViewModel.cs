using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballChairman.Models;
using FootballChairman.Models.Enums;
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
        private ICountryService _countryService;
        private ICompetitionCupService _competitionCupService;
        private IPlayerService _playerService;
        private ObservableCollection<Fixture> _fixtures = new ObservableCollection<Fixture>();

        public ObservableCollection<Fixture> Fixtures { get => _fixtures; }

        public FixturesPageViewModel(
            IFixtureService fixtureService,
            ICompetitionService competitionService,
            IClubService clubService,
            IClubPerCompetitionService clubPerCompetitionService,
            IManagerService managerService,
            ICountryService countryService,
            ICompetitionCupService competitionCupService,
            IPlayerService playerService)
        {
            _fixtureService = fixtureService;
            _competitionService = competitionService;
            _clubService = clubService;
            _clubPerCompetitionService = clubPerCompetitionService;
            _managerService = managerService;
            _countryService = countryService;
            _competitionCupService = competitionCupService;
            _playerService = playerService;

            CreateCountries();
            CreateCompetition();
            CreateClubs();
            CreateManagers();
            CreatePlayers();
            CreateClubsPerCompetitions();
            CreateFixtures();
            _managerService = managerService;

            //temp
            _playerService.CheckIfClubHasEnoughPlayers(69);
        }

        private void CreateCompetition()
        {
            if (_competitionService.GetAllCompetitions().Any())
                return;

            _competitionService.CreateCompetition(new Competition { Id = 0, Name = "Eerste Klasse", Reputation = 5000, PromotionCompetitionId = -1, RelegationCompetitionId = 1, NumberOfTeams = 10, CountryId = 0, CompetitionType = CompetitionType.NationalCompetition });
            _competitionService.CreateCompetition(new Competition { Id = 1, Name = "Tweede Klasse", Reputation = 4000, PromotionCompetitionId = 0, RelegationCompetitionId = 9, NumberOfTeams = 8, CountryId = 0, CompetitionType = CompetitionType.NationalCompetition });
            _competitionService.CreateCompetition(new Competition { Id = 2, Name = "Premier League", Reputation = 5000, PromotionCompetitionId = -1, RelegationCompetitionId = 3, NumberOfTeams = 10, CountryId = 1, CompetitionType = CompetitionType.NationalCompetition });
            _competitionService.CreateCompetition(new Competition { Id = 3, Name = "The Championship", Reputation = 4000, PromotionCompetitionId = 2, RelegationCompetitionId = -1, NumberOfTeams = 10, CountryId = 1, CompetitionType = CompetitionType.NationalCompetition });
            _competitionService.CreateCompetition(new Competition { Id = 4, Name = "Champions League", Reputation = 6000, PromotionCompetitionId = -1, RelegationCompetitionId = -1, CountryId = 2, CompetitionType = CompetitionType.InternationalCompetition });
            _competitionService.CreateCompetition(new Competition { Id = 5, Name = "Eredivisie", Reputation = 5000, PromotionCompetitionId = -1, RelegationCompetitionId = -1, NumberOfTeams = 6, CountryId = 3, CompetitionType = CompetitionType.NationalCompetition });
            _competitionService.CreateCompetition(new Competition { Id = 6, Name = "Ligue 1", Reputation = 5000, PromotionCompetitionId = -1, RelegationCompetitionId = -1, NumberOfTeams = 6, CountryId = 4, CompetitionType = CompetitionType.NationalCompetition });
            _competitionService.CreateCompetition(new Competition { Id = 7, Name = "Primera Division", Reputation = 5000, PromotionCompetitionId = -1, RelegationCompetitionId = -1, NumberOfTeams = 6, CountryId = 5, CompetitionType = CompetitionType.NationalCompetition });
            _competitionService.CreateCompetition(new Competition { Id = 8, Name = "1. Bundesliga", Reputation = 5000, PromotionCompetitionId = -1, RelegationCompetitionId = -1, NumberOfTeams = 6, CountryId = 6, CompetitionType = CompetitionType.NationalCompetition });
            _competitionService.CreateCompetition(new Competition { Id = 9, Name = "Derde Klasse", Reputation = 3000, PromotionCompetitionId = 1, RelegationCompetitionId = 10, NumberOfTeams = 8, CountryId = 0, CompetitionType = CompetitionType.NationalCompetition });
            _competitionService.CreateCompetition(new Competition { Id = 10, Name = "Vierde Klasse", Reputation = 2000, PromotionCompetitionId = 9, RelegationCompetitionId = 11, NumberOfTeams = 8, CountryId = 0, CompetitionType = CompetitionType.NationalCompetition });
            _competitionService.CreateCompetition(new Competition { Id = 11, Name = "Vijfde Klasse", Reputation = 1000, PromotionCompetitionId = 10, RelegationCompetitionId = -1, NumberOfTeams = 8, CountryId = 0, CompetitionType = CompetitionType.NationalCompetition });

            _competitionCupService.CreateCompetition(new CompetitionCup { Id = 12, Name = "Croky Cup", Reputation = 4500, CountryId = 0, CompetitionType = CompetitionType.NationalCup });
            _competitionCupService.CreateCompetition(new CompetitionCup { Id = 13, Name = "FA Cup", Reputation = 4500, CountryId = 1, CompetitionType = CompetitionType.NationalCup });
            _competitionCupService.CreateCompetition(new CompetitionCup { Id = 14, Name = "KNVB Cup", Reputation = 4500, CountryId = 3, CompetitionType = CompetitionType.NationalCup });
            _competitionCupService.CreateCompetition(new CompetitionCup { Id = 15, Name = "Coupe de France", Reputation = 4500, CountryId = 4, CompetitionType = CompetitionType.NationalCup });
            _competitionCupService.CreateCompetition(new CompetitionCup { Id = 16, Name = "Copa Del Rey", Reputation = 4500, CountryId = 5, CompetitionType = CompetitionType.NationalCup });
            _competitionCupService.CreateCompetition(new CompetitionCup { Id = 17, Name = "DFB Pokal", Reputation = 4500, CountryId = 6, CompetitionType = CompetitionType.NationalCup });
            _competitionCupService.CreateCompetition(new CompetitionCup { Id = 18, Name = "Cup Winners Cup", Reputation = 5000, CountryId = 2, CompetitionType = CompetitionType.InternationalCup });
        }

        private void CreateClubs()
        {
            if (_clubService.GetAllClubs().Any())
                return;
            var random = new Random();

            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 0, Name = "Union" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 1, Name = "Club Brugge" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 2, Name = "Antwerp" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 3, Name = "Anderlecht" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 4, Name = "AA Gent" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 5, Name = "KV Mechelen" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 6, Name = "Charleroi" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 7, Name = "RC Genk" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 8, Name = "Cercle Brugge" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 9, Name = "Sint Truiden" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 10, Name = "KV Kortrijk" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 11, Name = "OH Leuven" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 12, Name = "Standard" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 13, Name = "KV Oostende" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 14, Name = "Eupen" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 15, Name = "Zulte Waregem" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 16, Name = "Seraing" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 17, Name = "Beerschot" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 18, Name = "Manchester City" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 19, Name = "Liverpool" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 20, Name = "Chelsea" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 21, Name = "Arsenal" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 22, Name = "Manchester United" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 23, Name = "West Ham United" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 24, Name = "Tottenham" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 25, Name = "Wolves" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 26, Name = "Aston Villa" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 27, Name = "Southampton" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 28, Name = "Crystal Palace" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 29, Name = "Leicester City" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 30, Name = "Brighton" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 31, Name = "Newcastle United" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 32, Name = "Brentford" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 33, Name = "Leeds" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 34, Name = "Everton" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 35, Name = "Burnley" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 36, Name = "Watford" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 37, Name = "Norwich City" });
            _clubService.CreateClub(new Club { CountryId = 3, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 38, Name = "Ajax" });
            _clubService.CreateClub(new Club { CountryId = 3, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 39, Name = "PSV" });
            _clubService.CreateClub(new Club { CountryId = 3, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 40, Name = "Feyenoord" });
            _clubService.CreateClub(new Club { CountryId = 3, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 41, Name = "AZ" });
            _clubService.CreateClub(new Club { CountryId = 3, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 42, Name = "Twente" });
            _clubService.CreateClub(new Club { CountryId = 3, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 43, Name = "Vitesse" });
            _clubService.CreateClub(new Club { CountryId = 4, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 44, Name = "PSG" });
            _clubService.CreateClub(new Club { CountryId = 4, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 45, Name = "Marseille" });
            _clubService.CreateClub(new Club { CountryId = 4, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 46, Name = "Nice" });
            _clubService.CreateClub(new Club { CountryId = 4, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 47, Name = "Rennes" });
            _clubService.CreateClub(new Club { CountryId = 4, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 48, Name = "Strasbourg" });
            _clubService.CreateClub(new Club { CountryId = 4, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 49, Name = "LOSC" });
            _clubService.CreateClub(new Club { CountryId = 5, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 50, Name = "Real Madrid" });
            _clubService.CreateClub(new Club { CountryId = 5, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 51, Name = "Sevilla" });
            _clubService.CreateClub(new Club { CountryId = 5, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 52, Name = "Barcelona" });
            _clubService.CreateClub(new Club { CountryId = 5, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 53, Name = "Atlético Madrid" });
            _clubService.CreateClub(new Club { CountryId = 5, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 54, Name = "Real Betis" });
            _clubService.CreateClub(new Club { CountryId = 5, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 55, Name = "Real Sociedad" });
            _clubService.CreateClub(new Club { CountryId = 6, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 56, Name = "Bayern" });
            _clubService.CreateClub(new Club { CountryId = 6, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 57, Name = "Dortmund" });
            _clubService.CreateClub(new Club { CountryId = 6, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 58, Name = "Leverkusen" });
            _clubService.CreateClub(new Club { CountryId = 6, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 59, Name = "RB Leipzig" });
            _clubService.CreateClub(new Club { CountryId = 6, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 60, Name = "Freiburg" });
            _clubService.CreateClub(new Club { CountryId = 6, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 61, Name = "Hoffenheim" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 62, Name = "Westerlo" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 63, Name = "RWDM" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 64, Name = "Waasland-Beveren" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 65, Name = "Deinze" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 66, Name = "Moeskroen" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 67, Name = "Lierse Kempenzonen" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 68, Name = "Lommel" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 69, Name = "Virton" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 70, Name = "Dessel" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 71, Name = "Dender" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 72, Name = "FC Luik" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 73, Name = "Olympic Charleroi" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 74, Name = "Patro Eisden" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 75, Name = "Knokke" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 76, Name = "Heist" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 77, Name = "Sint Eloois-Winkel" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 78, Name = "Tienen" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 79, Name = "Wezet" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 80, Name = "Francs Borain" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 81, Name = "Rupel-Boom" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 82, Name = "Thes" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 83, Name = "Mandel United" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 84, Name = "La Louvière-Centre" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), SkillGoalkeeping = random.Next(1, 99), Id = 85, Name = "RC Hoboken", IsPlayer = true });

        }

        private void CreateClubsPerCompetitions()
        {
            if (_clubPerCompetitionService.GetAll().Any())
            {
                return;
            }

            var countries = _countryService.GetAllCountries();

            foreach (var country in countries)
            {
                var competitions = _competitionService.GetAllCompetitions().Where(com => com.CountryId == country.Id && com.CompetitionType == CompetitionType.NationalCompetition).ToList();
                var cupCompetition = _competitionCupService.GetAllCompetitions();
                int counter = 0;
                int competitionCounter = 0;
                foreach (var club in _clubService.GetAllClubs().Where(c => c.CountryId == country.Id))
                {
                    _clubPerCompetitionService.CreateClubPerCompetition(new ClubPerCompetition(club.Id, club.Name) { CompetitionId = competitions[competitionCounter].Id });
                    _clubPerCompetitionService.CreateClubPerCompetition(new ClubPerCompetition(club.Id, club.Name) { CompetitionId = cupCompetition.FirstOrDefault(com => com.CountryId == club.CountryId).Id });

                    counter++;
                    if (counter >= competitions[competitionCounter].NumberOfTeams)
                    {
                        counter = 0;
                        competitionCounter++;
                    }
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

            var competitionsCup = _competitionCupService.GetAllCompetitions();
            foreach (var competition in competitionsCup)
            {
                var listOfClubs = new List<Club>();

                foreach (var club in clubsPerCompetition.Where(c => c.CompetitionId == competition.Id))
                {
                    listOfClubs.Add(clubs.FirstOrDefault(c => c.Id == club.ClubId));
                }
                _fixtureService.GenerateCupFixtures(listOfClubs, competition);
            }
        }
        private void CreateManagers()
        {
            if (_managerService.GetAllManagers().Any())
                return;

            var clubs = _clubService.GetAllClubs();

            foreach (var club in clubs)
            {
                club.ManagerId = _managerService.GenerateManager(club.Id, club.CountryId).Id;
            }
            _clubService.CreateAllClubs(clubs);
        }

        private void CreatePlayers()
        {
            if (_playerService.GetPlayers().Any())
                return;

            var clubs = _clubService.GetAllClubs();

            foreach (var club in clubs)
            {
                for (int i = 0; i < 11; i++)
                {
                    _playerService.GenerateRandomPlayer(club.Id, club.CountryId);
                }
            }
        }
        private void CreateCountries()
        {
            if (_countryService.GetAllCountries().Any())
                return;

            _countryService.CreateCountry(new Country { Id = 0, Name = "Belgium" });
            _countryService.CreateCountry(new Country { Id = 1, Name = "England" });
            _countryService.CreateCountry(new Country { Id = 2, Name = "International" });
            _countryService.CreateCountry(new Country { Id = 3, Name = "Nederland" });
            _countryService.CreateCountry(new Country { Id = 4, Name = "Frankrijk" });
            _countryService.CreateCountry(new Country { Id = 5, Name = "Spanje" });
            _countryService.CreateCountry(new Country { Id = 6, Name = "Duitsland" });
        }
    }
}
