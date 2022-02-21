using FootballChairman.Pages;
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

        public FixturesPage FixturesPage => _fixturesPage ??= new FixturesPage();

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.NavigationService.Navigate(FixturesPage);

        }
    }
}
