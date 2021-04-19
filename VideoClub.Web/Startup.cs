using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VideoClub.Web.Startup))]
namespace VideoClub.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
