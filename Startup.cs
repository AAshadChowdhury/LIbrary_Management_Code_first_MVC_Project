using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CF__MVC_Project.Startup))]
namespace CF__MVC_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
