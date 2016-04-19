using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcMovie2.Startup))]
namespace MvcMovie2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
