using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(shoppinstore.Startup))]
namespace shoppinstore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
