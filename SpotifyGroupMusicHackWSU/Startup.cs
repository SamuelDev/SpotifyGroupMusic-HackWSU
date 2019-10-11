using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SpotifyGroupMusicHackWSU.Startup))]
namespace SpotifyGroupMusicHackWSU
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
