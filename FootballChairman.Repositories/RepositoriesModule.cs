using Autofac;

namespace FootballChairman.Repositories
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FixtureRepository>().AsImplementedInterfaces();
            builder.RegisterType<ClubRepository>().AsImplementedInterfaces();
            builder.RegisterType<CompetitionRepository>().AsImplementedInterfaces();
            builder.RegisterType<ClubPerCompetitionRepository>().AsImplementedInterfaces();
            builder.RegisterType<ManagerRepository>().AsImplementedInterfaces();
            builder.RegisterType<CountryRepository>().AsImplementedInterfaces();
            builder.RegisterType<HistoryItemRepository>().AsImplementedInterfaces();
            builder.RegisterType<CompetitionCupRepository>().AsImplementedInterfaces();
            builder.RegisterType<SaveGameDataRepository>().AsImplementedInterfaces();
            builder.RegisterType<GameRepository>().AsImplementedInterfaces();
            builder.RegisterType<PlayerRepository>().AsImplementedInterfaces();
            builder.RegisterType<PersonNameRepository>().AsImplementedInterfaces();
            builder.RegisterType<TacticRepository>().AsImplementedInterfaces();
            builder.RegisterType<TransferRepository>().AsImplementedInterfaces();
        }
    }
}
