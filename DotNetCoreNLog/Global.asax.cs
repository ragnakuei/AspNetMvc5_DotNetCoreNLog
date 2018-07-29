using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DotNetCoreNLog.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace DotNetCoreNLog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DependencyResolver.SetResolver(new DependencyInjectionResolver());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }

    public class DependencyInjectionResolver : IDependencyResolver
    {
        private static readonly ServiceProvider _provider;

        static DependencyInjectionResolver()
        {
            var service = new ServiceCollection();

            service.AddTransient<HomeController>();

            service.AddLogging(); // Support ILogger<T>
            service.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            service.AddTransient<ILoggerFactory>(s =>
                                                 {
                                                     var loggerFactory = new LoggerFactory();
                                                     loggerFactory.AddNLog();
                                                     return loggerFactory;
                                                 });
            _provider = service.BuildServiceProvider();
        }

        public object GetService(Type serviceType)
        {
            return _provider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _provider.GetServices(serviceType);
        }

    }
}
