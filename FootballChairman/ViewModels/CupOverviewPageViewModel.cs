using FootballChairman.Messages;
using FootballChairman.Models;
using FootballChairman.Models.Enums;
using FootballChairman.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using OlavFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.ViewModels
{
    public class CupOverviewPageViewModel : ViewModelBase
    {
        private ICountryService _countryService;
        private ICompetitionCupService _competitionCupService;
        private IFixtureService _fixtureService;
        private ISaveGameDataService _saveGameDataService;
        private IGameService _gameService;

        private string _saveGameName;

        private ObservableCollection<Game> _showLastGames;
        private ObservableCollection<CompetitionCup> _competitions;
        private ObservableCollection<Country> _countries;
        private ObservableCollection<Fixture> _showNextFixtures;

        private CompetitionCup _selectedCompetitionCup;
        private Country _selectedCountry;

        public ObservableCollection<CompetitionCup> Competitions { get => _competitions; set { _competitions = value; RaisePropertyChanged(); } }
        public ObservableCollection<Country> Countries { get => _countries; set { _countries = value; RaisePropertyChanged(); } }
        public ObservableCollection<Fixture> ShowNextFixtures { get => _showNextFixtures; set { _showNextFixtures = value; RaisePropertyChanged(); } }
        public ObservableCollection<Game> ShowLastGames { get => _showLastGames; set { _showLastGames = value; RaisePropertyChanged(); } }

        public string ScoreDevider { get => "-"; }

        public CompetitionCup SelectedCompetitionCup
        {
            get => _selectedCompetitionCup;
            set
            {
                _selectedCompetitionCup = value;
                if (_selectedCompetitionCup != null)
                {
                    LoadFixtureLists();
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

        public CupOverviewPageViewModel(
            ICountryService countryService,
            ICompetitionCupService competitionCupService,
            IFixtureService fixtureService,
            ISaveGameDataService saveGameDataService, 
            IGameService gameService)
        {
            _countryService = countryService;
            _competitionCupService = competitionCupService;
            _fixtureService = fixtureService;
            _saveGameDataService = saveGameDataService;
            _gameService = gameService;

            _saveGameName = Configuration.DefaultSaveGameName;

            Countries = new ObservableCollection<Country>(_countryService.GetAllCountries());
            SelectedCountry = Countries.FirstOrDefault();
            LoadCompetitions();

            Messenger.Default.Register<RefreshCompetitionData>(this, LoadData);
        }

        private void LoadData(RefreshCompetitionData obj)
        {
            LoadFixtureLists();
        }

        private void LoadCompetitions()
        {
            Competitions = new ObservableCollection<CompetitionCup>(_competitionCupService.GetAllCompetitions().Where(com => com.CountryId == SelectedCountry.Id && com.CompetitionType == CompetitionType.NationalCup));
            if (Competitions.Count > 0)
                SelectedCompetitionCup = Competitions[0];
        }

        private void LoadFixtureLists()
        {
            //NextFixtures = new ObservableCollection<Fixture>(_fixtureService.LoadFixturesOfMatchday(_saveGameDataService.GetSaveGameData(_saveGameName).MatchDay));
            //ShowNextFixtures = new ObservableCollection<Fixture>(NextFixtures.Where(f => f.CompetitionId == SelectedCompetitionCup.Id));

            //ShowLastGames = new ObservableCollection<Game>(LastGames.Where(g => g.Fixture.CompetitionId == SelectedCompetitionCup.Id));

            var nextFixtures = _fixtureService.LoadFixturesOfMatchday(_saveGameDataService.GetSaveGameData(_saveGameName).MatchDay).Where(f => f.CompetitionId == SelectedCompetitionCup.Id);
            ShowNextFixtures = new ObservableCollection<Fixture>(nextFixtures);

            var lastGames = _gameService.GetGames().Where(g => g.Fixture.CompetitionId == SelectedCompetitionCup.Id && g.Fixture.RoundNo == _saveGameDataService.GetSaveGameData(_saveGameName).MatchDay - 1);
            ShowLastGames = new ObservableCollection<Game>(lastGames);
        }
    }
}
