using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RCMMaps.Startup))]
namespace RCMMaps
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
