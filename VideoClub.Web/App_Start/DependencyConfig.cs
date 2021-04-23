using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using System.Web.Mvc;
using VideoClub.Common.Services;
using VideoClub.Core.Interfaces;
using VideoClub.Infrastructure.Data;
using VideoClub.Infrastructure.Services.Implementations;
using VideoClub.Infrastructure.Services.Interfaces;
using VideoClub.Web.Mappings;

namespace VideoClub.Web.App_Start
{
    public class DependencyConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // IMapper
            builder.Register(c => MapperInit.Init()).AsSelf().SingleInstance();
            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();
            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new Mappings.Profile.Profile());
            });
            builder.RegisterInstance(mapperConfig.CreateMapper())
              .As<IMapper>()
              .SingleInstance();

            // DbContext
            builder.RegisterType<VideoClubDbContext>().As<VideoClubDbContext>();

            // Controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // ActionFilters

            // Services            
            builder.RegisterType<LoggingService>().As<ILoggingService>().InstancePerRequest();
            builder.RegisterType<MovieService>().As<IMovieService>().InstancePerRequest();
            builder.RegisterType<RentingService>().As<IRentingService>().InstancePerRequest();
            builder.RegisterType<CopyService>().As<ICopyService>().InstancePerRequest();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}