using FootballChairman.Messages;
using FootballChairman.Models;
using FootballChairman.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace FootballChairman.ViewModels
{
    public class ManagerPageViewModel : ViewModelBase
    {
        private readonly IManagerService _managerService;
        private readonly IClubService _clubService;
        private Manager _selectedManager;
        private Club _playerClub;
        private RelayCommand _transferManagerCommand;
        private RelayCommand _searchManagerCommand;
        private bool _reachableManagers;


        private ObservableCollection<Manager> _managers;

        public Manager SelectedManager { get => _selectedManager; set { _selectedManager = value; } }
        public RelayCommand TransferManagerCommand => _transferManagerCommand ??= new RelayCommand(TransferManager);
        public RelayCommand SearchManagerCommand => _searchManagerCommand ??= new RelayCommand(LoadData);

        public ObservableCollection<Manager> Managers { get => _managers; set { _managers = value; RaisePropertyChanged(); } }
        public bool ReachableManagers { get => _reachableManagers; set { _reachableManagers = value; } }


        public ManagerPageViewModel(IManagerService managerService, IClubService clubService)
        {
            _managerService = managerService;
            _clubService = clubService;
            Messenger.Default.Register<RefreshManagerDataMessage>(this, LoadData);

            LoadData();
        }

        private void LoadData(RefreshManagerDataMessage obj)
        {
            LoadData();
        }

        private void LoadData()
        {
            Managers = new ObservableCollection<Manager>(_managerService.GetAllManagers());
            var clubs = _clubService.GetAllClubs();
            _playerClub = clubs.FirstOrDefault(c => c.IsPlayer);

            if (ReachableManagers)
            {
                var lessRepClubs = clubs.Where(c => c.Reputation < _playerClub.Reputation).ToList();
                Managers = new ObservableCollection<Manager>(Managers.Where(m => lessRepClubs.Any(c => c.Id == m.ClubId)));
            }
            foreach (var manager in Managers)
            {
                manager.ClubName = clubs.FirstOrDefault(c => c.Id == manager.ClubId).Name;
            }
        }

        private void TransferManager()
        {
            if (SelectedManager == null)
            {
                return;
            }

            var playerManager = _managerService.GetManagerFromClub(_playerClub.Id);
            var otherClub = _clubService.GetClub(SelectedManager.ClubId);

            if (_playerClub.Reputation < otherClub.Reputation)
            {
                return;
            }

            SelectedManager.ClubId = _playerClub.Id;
            playerManager.ClubId = otherClub.Id;

            _playerClub.ManagerId = SelectedManager.Id;
            otherClub.ManagerId = playerManager.Id;

            _clubService.UpdateClub(otherClub);
            _clubService.UpdateClub(_playerClub);

            _managerService.UpdateManager(SelectedManager);
            _managerService.UpdateManager(playerManager);

            MessageBox.Show(SelectedManager.FirstName + " " + SelectedManager.LastName + " transfered to " + _playerClub.Name);
            LoadData();
        }
    }
}
