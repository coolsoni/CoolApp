using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CabgemininMVC.Startup))]
namespace CabgemininMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
