﻿using System;
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
        private ICountryService _countryService;
        private ObservableCollection<Fixture> _fixtures = new ObservableCollection<Fixture>();

        public ObservableCollection<Fixture> Fixtures { get => _fixtures; }

        public FixturesPageViewModel(
            IFixtureService fixtureService,
            ICompetitionService competitionService,
            IClubService clubService,
            IClubPerCompetitionService clubPerCompetitionService, 
            IManagerService managerService,
            ICountryService countryService)
        {
            _fixtureService = fixtureService;
            _competitionService = competitionService;
            _clubService = clubService;
            _clubPerCompetitionService = clubPerCompetitionService;
            _managerService = managerService;
            _countryService = countryService;

            CreateCountries();
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

            _competitionService.CreateCompetition(new Competition { Id = 0, Name = "Eerste Klasse", Skill = 15, PromotionCompetitionId = -1, RelegationCompetitionId = 1, NumberOfTeams = 10, CountryId = 0 });
            _competitionService.CreateCompetition(new Competition { Id = 1, Name = "Tweede Klasse", Skill = 12, PromotionCompetitionId = 0, RelegationCompetitionId = -1, NumberOfTeams = 8, CountryId = 0 });
            _competitionService.CreateCompetition(new Competition { Id = 2, Name = "Premier League", Skill = 12, PromotionCompetitionId = -1, RelegationCompetitionId = 3, NumberOfTeams = 10, CountryId = 1 });
            _competitionService.CreateCompetition(new Competition { Id = 3, Name = "The Championship", Skill = 12, PromotionCompetitionId = 2, RelegationCompetitionId = -1, NumberOfTeams = 10, CountryId = 1 });
        }

        private void CreateClubs()
        {
            if (_clubService.GetAllClubs().Any())
                return;
            var random = new Random();

            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 0, Name = "Union" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 1, Name = "Club Brugge" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 2, Name = "Antwerp", IsPlayer = true }) ;
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 3, Name = "Anderlecht" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 4, Name = "AA Gent" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 5, Name = "KV Mechelen" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 6, Name = "Charleroi" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 7, Name = "RC Genk" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 8, Name = "Cercle Brugge" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 9, Name = "Sint Truiden" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 10, Name = "KV Kortrijk" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 11, Name = "OH Leuven" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 12, Name = "Standard" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 13, Name = "KV Oostende" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 14, Name = "Eupen" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 15, Name = "Zulte Waregem" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 16, Name = "Seraing" });
            _clubService.CreateClub(new Club { CountryId = 0, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 17, Name = "Beerschot" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 18, Name = "Manchester City" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 19, Name = "Liverpool" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 20, Name = "Chelsea" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 21, Name = "Arsenal" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 22, Name = "Manchester United" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 23, Name = "West Ham United" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 24, Name = "Tottenham" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 25, Name = "Wolves" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 26, Name = "Aston Villa" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 27, Name = "Southampton" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 28, Name = "Crystal Palace" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 29, Name = "Leicester City" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 30, Name = "Brighton" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 31, Name = "Newcastle United" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 32, Name = "Brentford" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 33, Name = "Leeds" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 34, Name = "Everton" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 35, Name = "Burnley" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 36, Name = "Watford" });
            _clubService.CreateClub(new Club { CountryId = 1, SkillDefense = random.Next(1, 99), SkillAttack = random.Next(1, 99), SkillMidfield = random.Next(1, 99), Id = 37, Name = "Norwich City" });
        }

        private void CreateClubsPerCompetitions()
        {
            if (_clubPerCompetitionService.GetAll().Any())
            {
                _clubPerCompetitionService.ResetData();
                return;
            }

            var countries = _countryService.GetAllCountries();

            foreach (var country in countries)
            {
                var competitions = _competitionService.GetAllCompetitions().Where(com => com.CountryId == country.Id).ToList();
                int counter = 0;
                int competitionCounter = 0;
                foreach (var club in _clubService.GetAllClubs().Where(c => c.CountryId == country.Id))
                {
                    _clubPerCompetitionService.CreateClubPerCompetition(new ClubPerCompetition(club.Id, club.Name) { CompetitionId = competitions[competitionCounter].Id });

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
        }
        private void CreateManagers()
        {
            if (_managerService.GetAllManagers().Any())
                return;

            var clubs = _clubService.GetAllClubs();

            foreach (var club in clubs)
            {
                club.ManagerId = _managerService.GenerateManager(club.Id).Id;
            }
            _clubService.CreateAllClubs(clubs);
        }
        private void CreateCountries()
        {
            if (_countryService.GetAllCountries().Any())
                return;

            _countryService.CreateCountry(new Country { Id = 0, Name = "Belgium" });
            _countryService.CreateCountry(new Country { Id = 1, Name = "England" });
        }
    }
}
