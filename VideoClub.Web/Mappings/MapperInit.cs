using AutoMapper;

namespace VideoClub.Web.Mappings
{
    public class MapperInit
    {
        public static IMapper Init()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Profile.Profile());
            });
            configuration.AssertConfigurationIsValid();
            return configuration.CreateMapper();
        }
    }
}