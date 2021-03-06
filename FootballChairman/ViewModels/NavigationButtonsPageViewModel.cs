using FootballChairman.Messages;
using FootballChairman.Messages.PageOpeners;
using FootballChairman.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FootballChairman.ViewModels
{
    public class NavigationButtonsViewModel : ViewModelBase
    {
        private readonly IGameEngineService _gameEngineService;
        private RelayCommand _openFixturesPageCommand;
        private RelayCommand _openMatchOverviewPageCommand;
        private RelayCommand _openClubPageCommand;
        private RelayCommand _openHistoryPageCommand;
        private RelayCommand _openCupOverviewPageCommand;
        private RelayCommand _openManagerPageCommand;
        private RelayCommand _openTransferPageCommand;

        private RelayCommand _continueCommand;
        private RelayCommand _endSeasonCommand;

        public RelayCommand OpenFixturesPageCommand => _openFixturesPageCommand ??= new RelayCommand(OpenFixturesPage);
        public RelayCommand OpenMatchOverviewPageCommand => _openMatchOverviewPageCommand ??= new RelayCommand(OpenMatchOverviewPage);
        public RelayCommand OpenClubPageCommand => _openClubPageCommand ??= new RelayCommand(OpenClubPage);
        public RelayCommand OpenHistoryPageCommand => _openHistoryPageCommand ??= new RelayCommand(OpenHistoryPage);
        public RelayCommand OpenCupOverviewPageCommand => _openCupOverviewPageCommand ??= new RelayCommand(OpenCupOverviewPage);
        public RelayCommand OpenManagerPageCommand => _openManagerPageCommand ??= new RelayCommand(OpenManagerPage);
        public RelayCommand OpenTransferPageCommand => _openTransferPageCommand ??= new RelayCommand(OpenTransferPage);

        public RelayCommand ContinueCommand => _continueCommand ??= new RelayCommand(Continue);
        public RelayCommand EndSeasonCommand => _endSeasonCommand ??= new RelayCommand(EndSeason);

        public NavigationButtonsViewModel(IGameEngineService gameEngineService)
        {
            _gameEngineService = gameEngineService;
        }

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

        private void OpenManagerPage()
        {
            MessengerInstance.Send(new OpenManagerPageMessage());
        }

        private void OpenTransferPage()
        {
            MessengerInstance.Send(new OpenTransferPageMessage());
        }

        private void Continue()
        {
            _gameEngineService.ProcessMatchDay();
            MessengerInstance.Send(new RefreshCompetitionData());
            MessengerInstance.Send(new RefreshManagerDataMessage());
            MessengerInstance.Send(new RefreshYourClubDataMessage());
        }

        private void EndSeason()
        {
            _gameEngineService.GoToEndOfSeason();
            MessengerInstance.Send(new RefreshCompetitionData());
            MessengerInstance.Send(new RefreshManagerDataMessage());
            MessengerInstance.Send(new RefreshYourClubDataMessage());
        }
    }
}
