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
        private RelayCommand _openClubPageCommand;
        private RelayCommand _openHistoryPageCommand;
        private RelayCommand _openCupOverviewPageCommand;

        public RelayCommand OpenFixturesPageCommand => _openFixturesPageCommand ??= new RelayCommand(OpenFixturesPage);
        public RelayCommand OpenMatchOverviewPageCommand => _openMatchOverviewPageCommand ??= new RelayCommand(OpenMatchOverviewPage);
        public RelayCommand OpenClubPageCommand => _openClubPageCommand ??= new RelayCommand(OpenClubPage);
        public RelayCommand OpenHistoryPageCommand => _openHistoryPageCommand ??= new RelayCommand(OpenHistoryPage);
        public RelayCommand OpenCupOverviewPageCommand => _openCupOverviewPageCommand ??= new RelayCommand(OpenCupOverviewPage);

        private void OpenFixturesPage()
        {
            MessengerInstance.Send(new OpenFixturesPageMessage());
        }

        private void OpenMatchOverviewPage()
        {
            MessengerInstance.Send(new OpenMatchOverviewPageMessage());
        }
        private void OpenClubPage()
        {
            MessengerInstance.Send(new OpenClubPageMessage());
        }

        private void OpenHistoryPage()
        {
            MessengerInstance.Send(new OpenHistoryPageMessage());
        }


        private void OpenCupOverviewPage()
        {
            MessengerInstance.Send(new OpenCupOverviewPageMessage());
        }
    }
}
