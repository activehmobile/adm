using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ActiveH_Device_Manager.Startup))]
namespace ActiveH_Device_Manager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
