using System.Web.Http;
using Owin;

namespace DominationsBot
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWebApi(new HttpConfiguration());
        }
    }
}