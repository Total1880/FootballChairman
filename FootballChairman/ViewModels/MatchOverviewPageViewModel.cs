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
        private IClubPerCompetitionService _clubPerCompetitionService;
        private int _matchDay;
        private ObservableCollection<Game> _lastGames;
        private ObservableCollection<Fixture> _nextFixtures;
        private ObservableCollection<ClubPerCompetition> _ranking;
        private RelayCommand _nextGameCommand;

        public ObservableCollection<Game> LastGames { get => _lastGames; set { _lastGames = value; RaisePropertyChanged(); } }
        public ObservableCollection<Fixture> NextFixtures { get => _nextFixtures; set { _nextFixtures = value; RaisePropertyChanged(); } }
        public ObservableCollection<ClubPerCompetition> Ranking { get => _ranking; set { _ranking = value; RaisePropertyChanged(); } }
        public RelayCommand NextGameCommand => _nextGameCommand ??= new RelayCommand(NextGame);
        public string ScoreDevider { get => "-"; }

        public MatchOverviewPageViewModel(IFixtureService fixtureService, IGameService gameService, IClubPerCompetitionService clubPerCompetitionService)
        {
            _fixtureService = fixtureService;
            _gameService = gameService;
            _clubPerCompetitionService = clubPerCompetitionService;

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
            RefreshRanking();
        }

        private void PlayGames()
        {
            LastGames = new ObservableCollection<Game>();

            foreach (var fixture in _fixtureService.LoadFixturesOfMatchday(_matchDay - 1))
            {
                var game = _gameService.PlayGame(fixture);
                LastGames.Add(game);
                _clubPerCompetitionService.UpdateData(game);
            }
        }

        private void RefreshRanking()
        {
            Ranking = new ObservableCollection<ClubPerCompetition>(_clubPerCompetitionService.GetAll()
                .OrderByDescending(c => c.GoalDifference)
                .OrderByDescending(c => c.Points));
        }
    }
}
