using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CajaPopular.Startup))]
namespace CajaPopular
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
