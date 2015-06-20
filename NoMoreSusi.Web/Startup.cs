using Microsoft.Owin;
using NoMoreSusi.Web;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace NoMoreSusi.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
