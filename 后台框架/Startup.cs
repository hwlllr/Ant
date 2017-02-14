using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(后台框架.Startup))]
namespace 后台框架
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
