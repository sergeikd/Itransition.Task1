using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Itransition.Task1.Web.Startup))]
namespace Itransition.Task1.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
