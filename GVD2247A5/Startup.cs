using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GVD2247A5.Startup))]

namespace GVD2247A5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
