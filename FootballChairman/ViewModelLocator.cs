using Autofac;
using FootballChairman.Services;
using FootballChairman.ViewModels;

namespace FootballChairman
{
    public class ViewModelLocator
    {
        private readonly IContainer _container;

        public ViewModelLocator()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ServicesModule>();

            builder.RegisterType<FixturesPageViewModel>().SingleInstance();
            builder.RegisterType<NavigationButtonsViewModel>().SingleInstance();
            builder.RegisterType<MatchOverviewPageViewModel>().SingleInstance();
            builder.RegisterType<ClubPageViewModel>().SingleInstance();
            builder.RegisterType<HistoryPageViewModel>().SingleInstance();
            builder.RegisterType<CupOverviewPageViewModel>().SingleInstance();
            builder.RegisterType<ManagerPageViewModel>().SingleInstance();
            builder.RegisterType<TransferPageViewModel>().SingleInstance();
            builder.RegisterType<PlayerPageViewModel>().SingleInstance();

            _container = builder.Build();
        }

        public FixturesPageViewModel FixturesPage => _container.Resolve<FixturesPageViewModel>();
        public NavigationButtonsViewModel NavigationButtons => _container.Resolve<NavigationButtonsViewModel>();
        public MatchOverviewPageViewModel MatchOverviewPage => _container.Resolve<MatchOverviewPageViewModel>();
        public ClubPageViewModel ClubPage => _container.Resolve<ClubPageViewModel>();
        public HistoryPageViewModel HistoryPage => _container.Resolve<HistoryPageViewModel>();
        public CupOverviewPageViewModel CupOverviewPage => _container.Resolve<CupOverviewPageViewModel>();
        public ManagerPageViewModel ManagerPage => _container.Resolve<ManagerPageViewModel>();
        public TransferPageViewModel TransferPage => _container.Resolve<TransferPageViewModel>();
        public PlayerPageViewModel PlayerPage => _container.Resolve<PlayerPageViewModel>();
    }
}
