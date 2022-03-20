using FootballChairman.Messages.PageOpeners;
using FootballChairman.Pages;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        public FixturesPage FixturesPage => _fixturesPage ??= new FixturesPage();
        public NavigationButtonsPage NavigationButtonsPage => _navigationButtonsPage ??= new NavigationButtonsPage();
        public MatchOverviewPage MatchOverviewPage => _matchOverviewPage ??= new MatchOverviewPage();
        public ClubPage ClubPage => _clubPage ??= new ClubPage();
        public HistoryPage HistoryPage => _historyPage ??= new HistoryPage();
        public CupOverviewPage CupOverviewPage => _cupOverviewPage ??= new CupOverviewPage();

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
    }
}
