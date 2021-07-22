using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestePratico_Odair.Startup))]
namespace TestePratico_Odair
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
