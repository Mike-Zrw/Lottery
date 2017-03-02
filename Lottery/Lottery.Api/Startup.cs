using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lottery.Api.Startup))]
namespace Lottery.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
