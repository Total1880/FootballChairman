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
        private int _matchDay;
        private ObservableCollection<Fixture> _lastFixtures;
        private ObservableCollection<Fixture> _nextFixtures;
        private RelayCommand _nextGameCommand;

        public ObservableCollection<Fixture> LastFixtures { get => _lastFixtures; set { _lastFixtures = value; RaisePropertyChanged(); } }
        public ObservableCollection<Fixture> NextFixtures { get => _nextFixtures; set { _nextFixtures = value; RaisePropertyChanged(); } }
        public RelayCommand NextGameCommand => _nextGameCommand ??= new RelayCommand(NextGame);

        public MatchOverviewPageViewModel(IFixtureService fixtureService)
        {
            _fixtureService = fixtureService;
            _matchDay = 1;
            LoadFixtureLists();
        }

        private void LoadFixtureLists()
        {
            LastFixtures = new ObservableCollection<Fixture>(_fixtureService.LoadFixturesOfMatchday(_matchDay - 1));
            NextFixtures = new ObservableCollection<Fixture>(_fixtureService.LoadFixturesOfMatchday(_matchDay));
        }

        private void NextGame()
        {
            _matchDay++;
            LoadFixtureLists();
        }
    }
}
