using FootballChairman.Models;
using FootballChairman.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FootballChairman.ViewModels
{
    public class TransferPageViewModel : ViewModelBase
    {
        private readonly IPlayerService _playerService;
        private readonly IClubService _clubService;
        private int _searchGoalkeepingMin;
        private int _searchGoalkeepingMax;
        private int _searchDefenseMin;
        private int _searchDefenseMax;
        private int _searchMidfieldMin;
        private int _searchMidfieldMax;
        private int _searchAttackMin;
        private int _searchAttackMax;
        private bool _reachablePlayers;

        private IList<Club> _clubs;
        private Player _selectedPlayer;
        private Club _playerClub;

        public Player SelectedPlayer { get => _selectedPlayer; set { _selectedPlayer = value; } }


        private ObservableCollection<Player> _players;

        public ObservableCollection<Player> Players { get => _players; set { _players = value; RaisePropertyChanged(); } }

        private RelayCommand _searchPlayersCommand;
        private RelayCommand _transferPlayersCommand;

        public RelayCommand SearchPlayersCommand => _searchPlayersCommand ??= new RelayCommand(SearchPlayers);
        public RelayCommand TransferPlayersCommand => _transferPlayersCommand ??= new RelayCommand(TransferPlayers);

        public int SearchGoalkeepingMin { get => _searchGoalkeepingMin; set { _searchGoalkeepingMin = value; } }
        public int SearchGoalkeepingMax { get => _searchGoalkeepingMax; set { _searchGoalkeepingMax = value; } }
        public int SearchDefenseMin { get => _searchDefenseMin; set { _searchDefenseMin = value;} }
        public int SearchDefenseMax { get => _searchDefenseMax; set { _searchDefenseMax = value;} }
        public int SearchMidfieldMin { get => _searchMidfieldMin; set { _searchMidfieldMin = value;} }
        public int SearchMidfieldMax { get => _searchMidfieldMax; set { _searchMidfieldMax = value;} }
        public int SearchAttackMin { get => _searchAttackMin; set { _searchAttackMin = value;} }
        public int SearchAttackMax { get => _searchAttackMax; set { _searchAttackMax = value;} }
        public bool ReachablePlayers { get => _reachablePlayers; set { _reachablePlayers = value;} }

        public TransferPageViewModel(IPlayerService playerService, IClubService clubService)
        {
            _playerService = playerService;
            _clubService = clubService;

            _clubs = _clubService.GetAllClubs();
            _playerClub = _clubs.FirstOrDefault(c => c.IsPlayer);

            SearchGoalkeepingMax = 100;
            SearchDefenseMax = 100;
            SearchMidfieldMax = 100;
            SearchAttackMax = 100;
        }

        private void SearchPlayers()
        {
            var listPlayers = _playerService
                .GetPlayers()
                .Where(p => p.Defense >= SearchDefenseMin && p.Defense <= SearchDefenseMax &&
                p.Midfield >= SearchMidfieldMin && p.Midfield <= SearchMidfieldMax &&
                p.Attack >= SearchAttackMin && p.Attack <= SearchAttackMax &&
                p.Goalkeeping >= SearchAttackMin && p.Goalkeeping <= SearchGoalkeepingMax);

            var searchPlayers = new List<Player>(listPlayers);

            foreach (Player player in searchPlayers)
            {
                var club = _clubs.FirstOrDefault(c => c.Id == player.ClubId);

                if (ReachablePlayers && club.Reputation > _playerClub.Reputation)
                {
                    listPlayers = listPlayers.Where(p => p.Id != player.Id).ToList();
                    continue;
                }

                listPlayers.FirstOrDefault(p => p.Id == player.Id).ClubName = club.Name;
            }

            Players = new ObservableCollection<Player>(listPlayers);
        }

        private void TransferPlayers()
        {
            if (SelectedPlayer == null)
                return;

            var clubReputation = _clubs.FirstOrDefault(c => c.Id == SelectedPlayer.ClubId).Reputation;

            if (clubReputation > _playerClub.Reputation)
                return;

            SelectedPlayer.ClubId = _playerClub.Id;

            _playerService.CreatePlayer(SelectedPlayer);

            MessageBox.Show(SelectedPlayer.FirstName + " " + SelectedPlayer.LastName + " has signed for " + _playerClub.Name);

            SearchPlayers();
        }
    }
}
