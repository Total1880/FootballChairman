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
    public class PlayerPageViewModel : ViewModelBase
    {
        private readonly IPlayerService _playerService;
        private readonly ITransferService _transferService;
        private readonly IClubService _clubService;
        private ObservableCollection<Transfer> _transfers;
        private Player _shownPlayer;

        public ObservableCollection<Transfer> Transfers { get => _transfers; set { _transfers = value; RaisePropertyChanged(); } }

        public Player ShownPlayer
        {
            get => _shownPlayer;
            set
            {
                _shownPlayer = value;
                if (_shownPlayer != null)
                {
                    _shownPlayer.ClubName = _clubService.GetClub(_shownPlayer.ClubId).Name;
                }
                RaisePropertyChanged();
            }
        }
        public PlayerPageViewModel(IPlayerService playerService, ITransferService transferService, IClubService clubService)
        {
            _playerService = playerService;
            _transferService = transferService;
            _clubService = clubService;

            Messenger.Default.Register<ViewThisPlayerMessage>(this, RefreshPlayerData);
        }

        private void RefreshPlayerData(ViewThisPlayerMessage obj)
        {
            ShownPlayer = _playerService.GetPlayer(obj.PlayerId);
            Transfers = new ObservableCollection<Transfer>(_transferService.GetTransferListOfPlayer(obj.PlayerId));
        }
    }
}
