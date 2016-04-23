//using Star.Web.WebApi;

using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;

namespace GCR
{
    public class WebApiConfigurator
    {
        private readonly HttpConfiguration _config;
        private readonly IDependencyResolver _dependencyResolver;
        private readonly GcrExceptionHandler _gcrExceptionHandler;


        public WebApiConfigurator(HttpConfiguration config, IDependencyResolver dependencyResolver,
            GcrExceptionHandler gcrExceptionHandler)
        {
            _config = config;
            _dependencyResolver = dependencyResolver;
            _gcrExceptionHandler = gcrExceptionHandler;
        }

        public void Configure()
        {
            _config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{action}/{uid}",
                new {uid = RouteParameter.Optional}
                );
            _config.DependencyResolver = _dependencyResolver;
            _config.Services.Replace(typeof (IExceptionHandler), _gcrExceptionHandler);
            _config.Services.Replace(typeof(IHttpControllerActivator),_dependencyResolver);
        }}
}