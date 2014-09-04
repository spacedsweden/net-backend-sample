using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SinchBackend.Startup))]
namespace SinchBackend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
