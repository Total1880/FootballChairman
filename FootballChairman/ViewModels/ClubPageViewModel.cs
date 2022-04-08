using FootballChairman.Messages;
using FootballChairman.Models;
using FootballChairman.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Linq;

namespace FootballChairman.ViewModels
{
    public class ClubPageViewModel : ViewModelBase
    {
        private readonly IClubService _clubService;
        private readonly IManagerService _managerService;
        private readonly ICountryService _countryService;
        private readonly IPlayerService _playerService;
        private readonly ITacticService _tacticService;
        private readonly ITransferService _transferService;
        private Club _selectedClub;
        private Manager _selectedManager;
        private Country _selectedCountry;
        private string _goalkeeper;
        private string _defenders;
        private string _midfielders;
        private string _attackers;

        private ObservableCollection<Club> _clubs;
        private ObservableCollection<Country> _countries;
        private ObservableCollection<Player> _players;
        private ObservableCollection<Transfer> _transfers;

        public ObservableCollection<Club> Clubs { get => _clubs; set { _clubs = value; RaisePropertyChanged(); } }
        public ObservableCollection<Country> Countries { get => _countries; set { _countries = value; RaisePropertyChanged(); } }
        public ObservableCollection<Player> Players { get => _players; set { _players = value; RaisePropertyChanged(); } }
        public ObservableCollection<Transfer> Transfers { get => _transfers; set { _transfers = value; RaisePropertyChanged(); } }
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
                    Players = new ObservableCollection<Player>(_playerService.GetPlayersFromClub(value.Id));
                    LoadTacticLabels();
                    LoadTransfers();
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
        public string Goalkeeper { get => _goalkeeper; set { _goalkeeper = value; RaisePropertyChanged(); } }
        public string Defenders { get => _defenders; set { _defenders = value; RaisePropertyChanged(); } }
        public string Midfielders { get => _midfielders; set { _midfielders = value; RaisePropertyChanged(); } }
        public string Attackers { get => _attackers; set { _attackers = value; RaisePropertyChanged(); } }


        public ClubPageViewModel(
            IClubService clubService,
            IManagerService managerService,
            ICountryService countryService,
            IPlayerService playerService,
            ITacticService tacticService,
            ITransferService transferService)
        {
            _clubService = clubService;
            _managerService = managerService;
            _countryService = countryService;
            _playerService = playerService;
            _tacticService = tacticService;
            _transferService = transferService;

            Countries = new ObservableCollection<Country>(_countryService.GetAllCountries());
            LoadData(new RefreshYourClubDataMessage());
            Messenger.Default.Register<RefreshYourClubDataMessage>(this, LoadData);
            if (Clubs.Any(c => c.IsPlayer))
            {
                SelectedClub = Clubs.FirstOrDefault(c => c.IsPlayer);
            }
        }

        private void LoadData(RefreshYourClubDataMessage obj)
        {
            int placeholderId = 0;
            if (SelectedClub != null)
            {
                placeholderId = SelectedClub.Id;
            }

            if (SelectedCountry == null)
            {
                SelectedCountry = Countries.FirstOrDefault();
            }

            Clubs = new ObservableCollection<Club>(_clubService.GetAllClubs().Where(c => c.CountryId == SelectedCountry.Id));

            if (SelectedClub != null)
            {
                SelectedClub = Clubs.FirstOrDefault(c => c.Id == placeholderId);
            }
            else if (Clubs.Any(c => c.IsPlayer))
            {
                SelectedClub = Clubs.FirstOrDefault(c => c.IsPlayer);
            }
            else
            {
                SelectedClub = Clubs.FirstOrDefault();
            }
        }

        private void LoadTacticLabels()
        {
            Goalkeeper = string.Empty;
            Defenders = string.Empty;
            Midfielders = string.Empty;
            Attackers = string.Empty;

            if (SelectedClub == null)
            {
                return;
            }

            var tactic = _tacticService.GetTactic(SelectedClub.Id);

            Goalkeeper = tactic.Goalkeeper.LastNameFirstLetterFirstName;
            Defenders = string.Join(" | ", tactic.Defenders.Select(p => p.LastNameFirstLetterFirstName));
            Midfielders = string.Join(" | ", tactic.Midfielders.Select(p => p.LastNameFirstLetterFirstName));
            Attackers = string.Join(" | ", tactic.Attackers.Select(p => p.LastNameFirstLetterFirstName));
        }

        private void LoadTransfers()
        {
            Transfers = new ObservableCollection<Transfer>(_transferService.GetTransferListOfClub(SelectedClub.Id));
        }
    }
}
