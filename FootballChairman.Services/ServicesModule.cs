using Autofac;
using FootballChairman.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
