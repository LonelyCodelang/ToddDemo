using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RestSwaggerUI.Startup))]
namespace RestSwaggerUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
