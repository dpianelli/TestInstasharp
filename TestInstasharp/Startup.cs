using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestInstasharp.Startup))]
namespace TestInstasharp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
