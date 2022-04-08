using Autofac;
using FootballChairman.Repositories;

namespace FootballChairman.Services
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<RepositoriesModule>();

            builder.RegisterType<ScheduleMakerService>().AsImplementedInterfaces();
            builder.RegisterType<FixtureService>().AsImplementedInterfaces();
            builder.RegisterType<GameServiceV2>().AsImplementedInterfaces();
            builder.RegisterType<ClubServiceV2>().AsImplementedInterfaces();
            builder.RegisterType<ClubPerCompetitionService>().AsImplementedInterfaces();
            builder.RegisterType<CompetitionService>().AsImplementedInterfaces();
            builder.RegisterType<ManagerService>().AsImplementedInterfaces();
            builder.RegisterType<CounrtyService>().AsImplementedInterfaces();
            builder.RegisterType<HistoryItemService>().AsImplementedInterfaces();
            builder.RegisterType<CompetitionCupService>().AsImplementedInterfaces();
            builder.RegisterType<GameEngineService>().AsImplementedInterfaces();
            builder.RegisterType<SaveGameDataService>().AsImplementedInterfaces();
            builder.RegisterType<PlayerService>().AsImplementedInterfaces();
            builder.RegisterType<PersonNameService>().AsImplementedInterfaces();
            builder.RegisterType<TacticService>().AsImplementedInterfaces();
            builder.RegisterType<TransferService>().AsImplementedInterfaces();
        }
    }
}
