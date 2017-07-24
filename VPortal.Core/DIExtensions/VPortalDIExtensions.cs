using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using VPortal.Core.Data;
using VPortal.Core.Data.Crud;
using VPortal.Core.Log;
using VPortal.Core.Log.Services;
using ILogger = VPortal.Core.Log.ILogger;

namespace VPortal.Core.DIExtensions
{
    public static class VPortalDIExtensions
    {
        public static IServiceCollection RegisterVPortalCoreServices(this IServiceCollection services,
           IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetService<IConfigurationRoot>();
            
            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // add the context resolver to the services
            services.AddHttpContextAccessor();

            // database factory is set in the DI Container at the start of application
            // Here Dialect is the type of db we are going to use
            IDatabaseFactory dbFactory = DatabaseFactory.GetFactory(Dialect.SQLServer, serviceProvider);
            services.AddSingleton(dbFactory);


            // Logger services are injected here
            services.TryAddSingleton<ILoggerSettings, LoggerSettings>();
            services.TryAddSingleton<ILogProvider, DefaultLogProvider>();
            services.TryAddSingleton<ILogger, FileBaseLogger>();


            var logger = serviceProvider.GetService<ILogger>();


            // Logger service is injected to get logs from file by date and current day
            services.AddTransient<ILoggerService, LoggerService>();

            IHostingEnvironment hostingEnvironment = serviceProvider.GetService<IHostingEnvironment>();

            return services;
        }
    }
}
