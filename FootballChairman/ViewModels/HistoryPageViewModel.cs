using FootballChairman.Models;
using FootballChairman.Services.Interfaces;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.ViewModels
{
    public class HistoryPageViewModel : ViewModelBase
    {
        private readonly ICompetitionService _competitionService;
        private readonly ICountryService _countryService;
        private readonly IHistoryItemService _historyItemService;
        private readonly IClubService _clubService;
        private Competition _selectedCompetition;
        private Country _selectedCountry;

        private ObservableCollection<Competition> _competitions;
        private ObservableCollection<Country> _countries;
        private ObservableCollection<HistoryItem> _historyItems;

        public ObservableCollection<Competition> Competitions { get => _competitions; set { _competitions = value; RaisePropertyChanged(); } }
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

        public Competition SelectedCompetition
        {
            get => _selectedCompetition;
            set
            {
                _selectedCompetition = value;
                if (value != null)
                    LoadDataHistoryItems();
                RaisePropertyChanged();
            }
        }

        public HistoryPageViewModel(ICompetitionService competitionService, ICountryService countryService, IHistoryItemService historyItemService, IClubService clubService)
        {
            _competitionService = competitionService;
            _countryService = countryService;
            _historyItemService = historyItemService;
            _clubService = clubService;

            Countries = new ObservableCollection<Country>(_countryService.GetAllCountries());

            LoadData();
        }

        private void LoadData()
        {
            if (SelectedCountry == null)
                SelectedCountry = Countries.FirstOrDefault();

            Competitions = new ObservableCollection<Competition>(_competitionService.GetAllCompetitions().Where(com => com.CountryId == SelectedCountry.Id));

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
