using FootballChairman.Models;
using FootballChairman.Services.Interfaces;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Linq;

namespace FootballChairman.ViewModels
{
    public class HistoryPageViewModel : ViewModelBase
    {
        private readonly ICompetitionService _competitionService;
        private readonly ICompetitionCupService _competitionCupService;
        private readonly ICountryService _countryService;
        private readonly IHistoryItemService _historyItemService;
        private readonly IClubService _clubService;
        private CompetitionBase _selectedCompetition;
        private Country _selectedCountry;

        private ObservableCollection<CompetitionBase> _competitions;
        private ObservableCollection<Country> _countries;
        private ObservableCollection<HistoryItem> _historyItems;

        public ObservableCollection<CompetitionBase> Competitions { get => _competitions; set { _competitions = value; RaisePropertyChanged(); } }
        public ObservableCollection<Country> Countries { get => _countries; set { _countries = value; RaisePropertyChanged(); } }
        public ObservableCollection<HistoryItem> HistoryItems { get => _historyItems; set { _historyItems = value; RaisePropertyChanged(); } }

        public Country SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                _selectedCountry = value;
                LoadData();
                RaisePropertyChanged();
            }
        }

        public CompetitionBase SelectedCompetition
        {
            get => _selectedCompetition;
            set
            {
                _selectedCompetition = value;
                if (value != null)
                {
                    LoadDataHistoryItems();
                }

                RaisePropertyChanged();
            }
        }

        public HistoryPageViewModel(ICompetitionService competitionService, ICompetitionCupService competitionCupService, ICountryService countryService, IHistoryItemService historyItemService, IClubService clubService)
        {
            _competitionService = competitionService;
            _competitionCupService = competitionCupService;
            _countryService = countryService;
            _historyItemService = historyItemService;
            _clubService = clubService;

            Countries = new ObservableCollection<Country>(_countryService.GetAllCountries());

            LoadData();
        }

        private void LoadData()
        {
            if (SelectedCountry == null)
            {
                SelectedCountry = Countries.FirstOrDefault();
            }

            var competitions = _competitionCupService.GetAllCompetitions().Where(com => com.CountryId == SelectedCountry.Id);

            Competitions = new ObservableCollection<CompetitionBase>(_competitionService.GetAllCompetitions().Where(com => com.CountryId == SelectedCountry.Id));
            foreach (var competition in competitions)
            {
                Competitions.Add(competition);
            }

            SelectedCompetition = Competitions.FirstOrDefault();
        }
        private void LoadDataHistoryItems()
        {
            HistoryItems = new ObservableCollection<HistoryItem>(_historyItemService.GetHistoryItemsOfCompetition(SelectedCompetition.Id));

            foreach (var item in HistoryItems)
            {
                item.ClubName = _clubService.GetClub(item.ClubId).Name;
            }
        }
    }
}
