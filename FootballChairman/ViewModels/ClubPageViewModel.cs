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
        private Club _selectedClub;
        private Manager _selectedManager;

        private ObservableCollection<Club> _clubs;

        public ObservableCollection<Club> Clubs { get => _clubs; set { _clubs = value; RaisePropertyChanged(); } }

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


        public ClubPageViewModel(IClubService clubService, IManagerService managerService)
        {
            _clubService = clubService;
            _managerService = managerService;

            LoadData(new RefreshYourClubDataMessage());
            Messenger.Default.Register<RefreshYourClubDataMessage>(this, LoadData);
        }

        private void LoadData(RefreshYourClubDataMessage obj)
        {
            int placeholderId = 0;
            if (SelectedClub != null)
             placeholderId = SelectedClub.Id;

            Clubs = new ObservableCollection<Club>(_clubService.GetAllClubs());

            if (SelectedClub != null)
                SelectedClub = Clubs.FirstOrDefault(c => c.Id == placeholderId);
            else
                SelectedClub = Clubs.FirstOrDefault(c => c.IsPlayer);
        }
    }
}
