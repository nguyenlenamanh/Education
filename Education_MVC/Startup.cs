using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Education_MVC.Startup))]
namespace Education_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
