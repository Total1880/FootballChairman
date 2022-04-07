using FootballChairman.Messages.PageOpeners;
using FootballChairman.Pages;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace FootballChairman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FixturesPage _fixturesPage;
        private NavigationButtonsPage _navigationButtonsPage;
        private MatchOverviewPage _matchOverviewPage;
        private ClubPage _clubPage;
        private HistoryPage _historyPage;
        private CupOverviewPage _cupOverviewPage;
        private ManagerPage _managerPage;
        private TransferPage _transferPage;

        public FixturesPage FixturesPage => _fixturesPage ??= new FixturesPage();
        public NavigationButtonsPage NavigationButtonsPage => _navigationButtonsPage ??= new NavigationButtonsPage();
        public MatchOverviewPage MatchOverviewPage => _matchOverviewPage ??= new MatchOverviewPage();
        public ClubPage ClubPage => _clubPage ??= new ClubPage();
        public HistoryPage HistoryPage => _historyPage ??= new HistoryPage();
        public CupOverviewPage CupOverviewPage => _cupOverviewPage ??= new CupOverviewPage();
        public ManagerPage ManagerPage => _managerPage ??= new ManagerPage();
        public TransferPage TransferPage => _transferPage ??= new TransferPage();

        public MainWindow()
        {
            InitializeComponent();


            MainFrame.NavigationService.Navigate(FixturesPage);
            NavigationFrame.NavigationService.Navigate(NavigationButtonsPage);

            Messenger.Default.Register<OpenFixturesPageMessage>(this, OpenFixturePage);
            Messenger.Default.Register<OpenMatchOverviewPageMessage>(this, OpenMatchOverviewPage);
            Messenger.Default.Register<OpenClubPageMessage>(this, OpenClubPage);
            Messenger.Default.Register<OpenHistoryPageMessage>(this, OpenHistoryPage);
            Messenger.Default.Register<OpenCupOverviewPageMessage>(this, OpenCupOverviewPage);
            Messenger.Default.Register<OpenManagerPageMessage>(this, OpenManagerPage);
            Messenger.Default.Register<OpenTransferPageMessage>(this, OpenTransferPage);
        }

        private void OpenFixturePage(OpenFixturesPageMessage obj)
        {
            MainFrame.NavigationService.Navigate(FixturesPage);
        }

        private void OpenMatchOverviewPage(OpenMatchOverviewPageMessage obj)
        {
            MainFrame.NavigationService.Navigate(MatchOverviewPage);
        }

        private void OpenClubPage(OpenClubPageMessage obj)
        {
            MainFrame.NavigationService.Navigate(ClubPage);
        }

        private void OpenHistoryPage(OpenHistoryPageMessage obj)
        {
            MainFrame.NavigationService.Navigate(HistoryPage);
        }

        private void OpenCupOverviewPage(OpenCupOverviewPageMessage obj)
        {
            MainFrame.NavigationService.Navigate(CupOverviewPage);
        }

        private void OpenManagerPage(OpenManagerPageMessage obj)
        {
            MainFrame.NavigationService.Navigate(ManagerPage);
        }

        private void OpenTransferPage(OpenTransferPageMessage obj)
        {
            MainFrame.NavigationService.Navigate(TransferPage);

        }
    }
}
