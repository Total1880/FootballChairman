using FootballChairman.Messages;
using FootballChairman.Models;
using FootballChairman.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.ViewModels
{
    public class ClubPageViewModel : ViewModelBase
    {
        private readonly IClubService _clubService;
        private readonly IManagerService _managerService;
        private readonly ICountryService _countryService;
        private Club _selectedClub;
        private Manager _selectedManager;
        private Country _selectedCountry;

        private ObservableCollection<Club> _clubs;
        private ObservableCollection<Country> _countries;

        public ObservableCollection<Club> Clubs { get => _clubs; set { _clubs = value; RaisePropertyChanged(); } }
        public ObservableCollection<Country> Countries { get => _countries; set { _countries = value; RaisePropertyChanged(); } }
        public Country SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                _selectedCountry = value;
                LoadData(new RefreshYourClubDataMessage());
                RaisePropertyChanged();
            }
        }

        public Club SelectedClub
        {
            get => _selectedClub;
            set
            {
                _selectedClub = value;
                if (_selectedClub != null)
                {
                    SelectedManager = _managerService.GetManager(value.ManagerId);
                }
                RaisePropertyChanged();
            }
        }

        public Manager SelectedManager
        {
            get => _selectedManager;
            set
            {
                _selectedManager = value;
                RaisePropertyChanged();
            }
        }


        public ClubPageViewModel(IClubService clubService, IManagerService managerService, ICountryService countryService)
        {
            _clubService = clubService;
            _managerService = managerService;
            _countryService = countryService;

            Countries = new ObservableCollection<Country>(_countryService.GetAllCountries());
            LoadData(new RefreshYourClubDataMessage());
            Messenger.Default.Register<RefreshYourClubDataMessage>(this, LoadData);
        }

        private void LoadData(RefreshYourClubDataMessage obj)
        {
            int placeholderId = 0;
            if (SelectedClub != null)
             placeholderId = SelectedClub.Id;
            if (SelectedCountry == null)
                SelectedCountry = Countries.FirstOrDefault();

            Clubs = new ObservableCollection<Club>(_clubService.GetAllClubs().Where(c => c.CountryId == SelectedCountry.Id));

            if (SelectedClub != null)
                SelectedClub = Clubs.FirstOrDefault(c => c.Id == placeholderId);
            else if (Clubs.Any(c => c.IsPlayer))
                SelectedClub = Clubs.FirstOrDefault(c => c.IsPlayer);
            else
                SelectedClub = Clubs.FirstOrDefault();
        }
    }
}
