using Microsoft.Owin;
using Owin;
using WebForLink.Web;

[assembly: OwinStartup(typeof (Startup))]

namespace WebForLink.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}