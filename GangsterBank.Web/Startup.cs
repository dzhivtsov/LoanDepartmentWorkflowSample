using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GangsterBank.Startup))]
namespace GangsterBank
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
