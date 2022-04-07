using FootballChairman.Messages;
using FootballChairman.Models;
using FootballChairman.Models.Enums;
using FootballChairman.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using OlavFramework;
using System.Collections.ObjectModel;
using System.Linq;

namespace FootballChairman.ViewModels
{
    public class MatchOverviewPageViewModel : ViewModelBase
    {
        private readonly IFixtureService _fixtureService;
        private readonly IGameService _gameService;
        private readonly IClubPerCompetitionService _clubPerCompetitionService;
        private readonly ICompetitionService _competitionService;
        private readonly ICountryService _countryService;
        private readonly ISaveGameDataService _saveGameDataService;
        private ObservableCollection<Game> _showLastGames;
        private ObservableCollection<Fixture> _showNextFixtures;
        private ObservableCollection<ClubPerCompetition> _ranking;
        private ObservableCollection<Competition> _competitions;
        private ObservableCollection<Country> _countries;
        private Competition _selectedCompetition;
        private Country _selectedCountry;
        private readonly string _saveGameName;

        public ObservableCollection<Game> ShowLastGames { get => _showLastGames; set { _showLastGames = value; RaisePropertyChanged(); } }
        public ObservableCollection<Fixture> ShowNextFixtures { get => _showNextFixtures; set { _showNextFixtures = value; RaisePropertyChanged(); } }
        public ObservableCollection<ClubPerCompetition> Ranking { get => _ranking; set { _ranking = value; RaisePropertyChanged(); } }
        public ObservableCollection<Competition> Competitions { get => _competitions; set { _competitions = value; RaisePropertyChanged(); } }
        public ObservableCollection<Country> Countries { get => _countries; set { _countries = value; RaisePropertyChanged(); } }

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
            ICompetitionService competitionService,
            ICountryService countryService,
            ISaveGameDataService saveGameDataService)
        {
            _fixtureService = fixtureService;
            _gameService = gameService;
            _clubPerCompetitionService = clubPerCompetitionService;
            _competitionService = competitionService;
            _countryService = countryService;
            _saveGameDataService = saveGameDataService;

            _saveGameName = Configuration.DefaultSaveGameName;
            Countries = new ObservableCollection<Country>(_countryService.GetAllCountries());
            SelectedCountry = Countries.FirstOrDefault();
            LoadCompetitions();
            LoadFixtureLists();

            Messenger.Default.Register<RefreshCompetitionData>(this, LoadData);
        }

        private void LoadData(RefreshCompetitionData obj)
        {
            LoadFixtureLists();
            RefreshRanking();
        }

        private void LoadFixtureLists()
        {
            var nextFixtures = _fixtureService.LoadFixturesOfMatchday(_saveGameDataService.GetSaveGameData(_saveGameName).MatchDay).Where(f => f.CompetitionId == SelectedCompetition.Id);
            ShowNextFixtures = new ObservableCollection<Fixture>(nextFixtures);

            var lastGames = _gameService.GetGames().Where(g => g.Fixture.CompetitionId == SelectedCompetition.Id && g.Fixture.RoundNo == _saveGameDataService.GetSaveGameData(_saveGameName).MatchDay - 1);
            ShowLastGames = new ObservableCollection<Game>(lastGames);
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
            Competitions = new ObservableCollection<Competition>(_competitionService.GetAllCompetitions().Where(com => com.CountryId == SelectedCountry.Id && com.CompetitionType != CompetitionType.NationalCup));
            SelectedCompetition = Competitions[0];
        }
    }
}
