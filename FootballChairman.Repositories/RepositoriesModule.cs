using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
