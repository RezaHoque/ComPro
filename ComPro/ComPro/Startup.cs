using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ComPro.Startup))]
namespace ComPro
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
