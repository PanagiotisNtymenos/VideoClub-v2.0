using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using VideoClub.Common.Services;
using VideoClub.Core.Interfaces;
using VideoClub.Infrastructure.Data;
using VideoClub.Web.Mappings;

namespace VideoClub.Web.App_Start
{
    public class DependencyConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // IMapper
            // builder.Register(c => MapperInit.Configuration()).AsSelf().SingleInstance();
            // builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();

            // DbContext
            builder.RegisterType<VideoClubDbContext>().As<VideoClubDbContext>();

            // Controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // ActionFilters

            // Services            
            // builder.RegisterAssemblyTypes(Assembly.Load(nameof(VideoClub.Common)))
            //  .Where(t => t.Namespace.Contains("Services"))
            //  .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));
            builder.RegisterType<MovieService>().As<IMovieService>().InstancePerRequest();
            builder.RegisterType<RentingService>().As<IRentingService>().InstancePerRequest();
            builder.RegisterType<CopyService>().As<ICopyService>().InstancePerRequest();
            builder.RegisterType<MovieGenreService>().As<IMovieGenreService>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}