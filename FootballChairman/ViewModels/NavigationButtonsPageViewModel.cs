using FootballChairman.Messages.PageOpeners;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.ViewModels
{
    public class NavigationButtonsViewModel : ViewModelBase
    {
        private RelayCommand _openFixturesPageCommand;
        private RelayCommand _openMatchOverviewPageCommand;

        public RelayCommand OpenFixturesPageCommand => _openFixturesPageCommand ??= new RelayCommand(OpenFixturesPage);
        public RelayCommand OpenMatchOverviewPageCommand => _openMatchOverviewPageCommand ??= new RelayCommand(OpenMatchOverviewPage);

        private void OpenFixturesPage()
        {
            MessengerInstance.Send(new OpenFixturesPageMessage());
        }

        private void OpenMatchOverviewPage()
        {
            MessengerInstance.Send(new OpenMatchOverviewPageMessage());

        }
    }
}
