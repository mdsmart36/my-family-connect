using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyFamilyConnect.Startup))]
namespace MyFamilyConnect
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
