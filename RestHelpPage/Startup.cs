using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RestHelpPage.Startup))]
namespace RestHelpPage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
