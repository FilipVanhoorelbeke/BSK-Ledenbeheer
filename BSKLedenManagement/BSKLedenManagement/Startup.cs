using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BSKLedenManagement.Startup))]
namespace BSKLedenManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
