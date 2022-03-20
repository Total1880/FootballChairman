using FootballChairman.Models;
using FootballChairman.Models.Enums;
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
    public class CupOverviewPageViewModel : ViewModelBase
    {
        private ICountryService _countryService;
        private ICompetitionCupService _competitionCupService;

        private ObservableCollection<CompetitionCup> _competitions;
        private ObservableCollection<Country> _countries;

        private CompetitionCup _selectedCompetitionCup;
        private Country _selectedCountry;

        public ObservableCollection<CompetitionCup> Competitions { get => _competitions; set { _competitions = value; RaisePropertyChanged(); } }
        public ObservableCollection<Country> Countries { get => _countries; set { _countries = value; RaisePropertyChanged(); } }

        public string ScoreDevider { get => "-"; }

        public CompetitionCup SelectedCompetitionCup
        {
            get => _selectedCompetitionCup;
            set
            {
                _selectedCompetitionCup = value;
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

        public CupOverviewPageViewModel(ICountryService countryService, ICompetitionCupService competitionCupService)
        {
            _countryService = countryService;
            _competitionCupService = competitionCupService;

            Countries = new ObservableCollection<Country>(_countryService.GetAllCountries());
            SelectedCountry = Countries.FirstOrDefault();
            LoadCompetitions();
        }

        private void LoadCompetitions()
        {
            Competitions = new ObservableCollection<CompetitionCup>(_competitionCupService.GetAllCompetitions().Where(com => com.CountryId == SelectedCountry.Id && com.CompetitionType == CompetitionType.NationalCup));
            if (Competitions.Count > 0)
                SelectedCompetitionCup = Competitions[0];
        }
    }
}
