﻿using FootballChairman.Messages;
using FootballChairman.Models;
using FootballChairman.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FootballChairman.ViewModels
{
    public class MatchOverviewPageViewModel : ViewModelBase
    {
        private IFixtureService _fixtureService;
        private IGameService _gameService;
        private IClubPerCompetitionService _clubPerCompetitionService;
        private IClubService _clubService;
        private ICompetitionService _competitionService;
        private IManagerService _managerService;
        private ICountryService _countryService;
        private IHistoryItemService _historyItemService;
        private int _matchDay;
        private ObservableCollection<Game> _lastGames;
        private ObservableCollection<Game> _showLastGames;
        private ObservableCollection<Fixture> _nextFixtures;
        private ObservableCollection<Fixture> _showNextFixtures;
        private ObservableCollection<ClubPerCompetition> _ranking;
        private ObservableCollection<Competition> _competitions;
        private ObservableCollection<Country> _countries;
        private RelayCommand _nextGameCommand;
        private RelayCommand _endSeasonCommand;
        private Visibility _showNextGameButton;
        private Visibility _showEndSeasonButton;
        private Competition _selectedCompetition;
        private Country _selectedCountry;
        private int _year = 1;

        public ObservableCollection<Game> LastGames { get => _lastGames; set { _lastGames = value; RaisePropertyChanged(); } }
        public ObservableCollection<Game> ShowLastGames { get => _showLastGames; set { _showLastGames = value; RaisePropertyChanged(); } }
        public ObservableCollection<Fixture> NextFixtures { get => _nextFixtures; set { _nextFixtures = value; RaisePropertyChanged(); } }
        public ObservableCollection<Fixture> ShowNextFixtures { get => _showNextFixtures; set { _showNextFixtures = value; RaisePropertyChanged(); } }
        public ObservableCollection<ClubPerCompetition> Ranking { get => _ranking; set { _ranking = value; RaisePropertyChanged(); } }
        public ObservableCollection<Competition> Competitions { get => _competitions; set { _competitions = value; RaisePropertyChanged(); } }
        public ObservableCollection<Country> Countries { get => _countries; set { _countries = value; RaisePropertyChanged(); } }
        public RelayCommand NextGameCommand => _nextGameCommand ??= new RelayCommand(NextGame);
        public RelayCommand EndSeasonCommand => _endSeasonCommand ??= new RelayCommand(EndSeason);

        public Visibility ShowNextGameButton { get => _showNextGameButton; set { _showNextGameButton = value; RaisePropertyChanged(); } }
        public Visibility ShowEndSeasonButton { get => _showEndSeasonButton; set { _showEndSeasonButton = value; RaisePropertyChanged(); } }

        public string ScoreDevider { get => "-"; }

        public Competition SelectedCompetition
        {
            get => _selectedCompetition;
            set
            {
                _selectedCompetition = value;
                if (_selectedCompetition != null)
                {
                    LoadFixtureLists();
                    RefreshRanking();
                }
                RaisePropertyChanged();
            }
        }

        public Country SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                _selectedCountry = value;
                LoadCompetitions();
            }
        }

        public MatchOverviewPageViewModel(
            IFixtureService fixtureService,
            IGameService gameService,
            IClubPerCompetitionService clubPerCompetitionService,
            IClubService clubService,
            ICompetitionService competitionService,
            IManagerService managerService,
            ICountryService countryService, IHistoryItemService historyItemService)
        {
            _fixtureService = fixtureService;
            _gameService = gameService;
            _clubPerCompetitionService = clubPerCompetitionService;
            _clubService = clubService;
            _competitionService = competitionService;
            _managerService = managerService;
            _countryService = countryService;
            _historyItemService = historyItemService;

            ShowEndSeasonButton = Visibility.Collapsed;
            ShowNextGameButton = Visibility.Visible;
            _matchDay = 1;
            Countries = new ObservableCollection<Country>(_countryService.GetAllCountries());
            SelectedCountry = Countries.FirstOrDefault();
            LoadCompetitions();
            LoadFixtureLists();

            //load gameyear
            var list = _historyItemService.GetHistoryItemsOfCompetition(Competitions[0].Id);
            if (list.Any())
                _year = list.Max(x => x.Year) + 1;
        }

        private void LoadFixtureLists()
        {
            NextFixtures = new ObservableCollection<Fixture>(_fixtureService.LoadFixturesOfMatchday(_matchDay));
            ShowNextFixtures = new ObservableCollection<Fixture>(NextFixtures.Where(f => f.CompetitionId == SelectedCompetition.Id));

            if (LastGames != null)
                ShowLastGames = new ObservableCollection<Game>(LastGames.Where(g => g.Fixture.CompetitionId == SelectedCompetition.Id));
        }

        private void NextGame()
        {
            _matchDay++;
            PlayGames();
            LoadFixtureLists();
            RefreshRanking();

            if (!NextFixtures.Any())
            {
                ShowEndSeasonButton = Visibility.Visible;
                ShowNextGameButton = Visibility.Collapsed;
            }
        }

        private void EndSeason()
        {
            var originalSelectedCompetitionId = SelectedCompetition.Id;

            _clubService.UpdateClubsEndOfSeasonTroughManager();
            _clubService.UpdateClubsWithNewManagers(_managerService.UpdateManagersEndSeason());

            var competitions = _competitionService.GetAllCompetitions();

            foreach (var competition in competitions)
            {
                SelectedCompetition = competition;
                _historyItemService.CreateHistoryItem(new HistoryItem { ClubId = Ranking[0].ClubId, CompetitionId = competition.Id, Year = _year });
                _clubPerCompetitionService.UpdatePromotionsAndRelegations(Ranking);
            }

            SelectedCompetition = Competitions.FirstOrDefault(c => c.Id == originalSelectedCompetitionId);

            ResetFixtures();

            _clubPerCompetitionService.ResetData();
            ShowEndSeasonButton = Visibility.Collapsed;
            ShowNextGameButton = Visibility.Visible;
            _matchDay = 1;
            LoadFixtureLists();
            RefreshRanking();

            MessengerInstance.Send(new RefreshYourClubDataMessage());
            _year++;
        }

        private void ResetFixtures()
        {
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

                _fixtureService.GenerateFixtures(listOfClubs, competition.Id);
            }
        }
        private void PlayGames()
        {
            LastGames = new ObservableCollection<Game>();

            foreach (var fixture in _fixtureService.LoadFixturesOfMatchday(_matchDay - 1))
            {
                var game = _gameService.PlayGame(fixture);
                LastGames.Add(game);
                _clubPerCompetitionService.UpdateData(game);
            }
        }

        private void RefreshRanking()
        {
            Ranking = new ObservableCollection<ClubPerCompetition>(_clubPerCompetitionService.GetAll()
                .Where(c => c.CompetitionId == SelectedCompetition.Id && !c.IsNew)
                .OrderBy(c => c.ClubName)
                .OrderByDescending(c => c.GoalDifference)
                .OrderByDescending(c => c.Points));
        }
        private void LoadCompetitions()
        {
            Competitions = new ObservableCollection<Competition>(_competitionService.GetAllCompetitions().Where(com => com.CountryId == SelectedCountry.Id));
            SelectedCompetition = Competitions[0];
        }
    }
}
