using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballChairman.Models;
using FootballChairman.Services.Interfaces;
using GalaSoft.MvvmLight;

namespace FootballChairman.ViewModels
{
    public class FixturesPageViewModel : ViewModelBase
    {
        private IScheduleMakerService _scheduleMakerService;
        private ObservableCollection<Fixture> _fixtures = new ObservableCollection<Fixture>();

        public ObservableCollection<Fixture>Fixtures{ get => _fixtures; }

        public FixturesPageViewModel(IScheduleMakerService scheduleMakerService)
        {
            _scheduleMakerService = scheduleMakerService;

            CreateFixtures();
        }

        private void CreateFixtures()
        {
            IList<string> _teams = new List<string>();

            _teams.Add("Antwerp");
            _teams.Add("Anderlecht");
            _teams.Add("Club Brugge");
            _teams.Add("Union");
            _teams.Add("AA Gent");
            _teams.Add("RC Genk");

            _fixtures = new ObservableCollection<Fixture>(_scheduleMakerService.Generate(_teams));
        }
    }
}
