using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LogicUni.Startup))]
namespace LogicUni
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
