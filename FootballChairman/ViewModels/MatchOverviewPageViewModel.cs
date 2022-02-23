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

namespace FootballChairman.ViewModels
{
    public class MatchOverviewPageViewModel : ViewModelBase
    {
        private IFixtureService _fixtureService;
        private IGameService _gameService;
        private int _matchDay;
        private ObservableCollection<Game> _lastGames;
        private ObservableCollection<Fixture> _nextFixtures;
        private RelayCommand _nextGameCommand;

        public ObservableCollection<Game> LastGames { get => _lastGames; set { _lastGames = value; RaisePropertyChanged(); } }
        public ObservableCollection<Fixture> NextFixtures { get => _nextFixtures; set { _nextFixtures = value; RaisePropertyChanged(); } }
        public RelayCommand NextGameCommand => _nextGameCommand ??= new RelayCommand(NextGame);
        public string ScoreDevider { get => "-"; }

        public MatchOverviewPageViewModel(IFixtureService fixtureService, IGameService gameService)
        {
            _fixtureService = fixtureService;
            _gameService = gameService;
            _matchDay = 1;
            LoadFixtureLists();
        }

        private void LoadFixtureLists()
        {
            NextFixtures = new ObservableCollection<Fixture>(_fixtureService.LoadFixturesOfMatchday(_matchDay));
        }

        private void NextGame()
        {
            _matchDay++;
            LoadFixtureLists();
            PlayGames();
        }

        private void PlayGames()
        {
            LastGames = new ObservableCollection<Game>();

            foreach (var fixture in _fixtureService.LoadFixturesOfMatchday(_matchDay - 1))
            {
                LastGames.Add(_gameService.PlayGame(fixture));
            }
        }
    }
}
