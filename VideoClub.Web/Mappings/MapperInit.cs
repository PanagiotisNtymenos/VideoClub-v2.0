using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoClub.Web.Mappings.Profile;

namespace VideoClub.Web.Mappings
{
    public class MapperInit
    {
        public static IMapper Init()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MovieProfile());
            });
            configuration.AssertConfigurationIsValid();
            return configuration.CreateMapper();
        }
    }
}