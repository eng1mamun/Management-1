using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WppAI.Startup))]
namespace WppAI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
