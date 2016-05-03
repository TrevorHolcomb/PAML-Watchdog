using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WatchdogUserPortal.Startup))]
namespace WatchdogUserPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
