using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TesteBematech.Startup))]
namespace TesteBematech
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
