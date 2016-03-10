using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CERBookingSystem.Startup))]
namespace CERBookingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
