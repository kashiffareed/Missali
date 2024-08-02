using Microsoft.Owin;
using Owin;


[assembly: OwinStartupAttribute("HandsWebConfig", typeof(Hands.Web.Startup))]
namespace Hands.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
