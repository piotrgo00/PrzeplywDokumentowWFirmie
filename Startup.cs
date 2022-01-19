using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PrzeplywDokumentowWFirmie.Startup))]
namespace PrzeplywDokumentowWFirmie
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
