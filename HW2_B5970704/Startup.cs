using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HW2_B5970704.Startup))]
namespace HW2_B5970704
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
