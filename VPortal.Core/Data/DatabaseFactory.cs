using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using VPortal.Core.Data.Crud;
using VPortal.Core.Data.MsSQL;
using Microsoft.Extensions.DependencyInjection;

namespace VPortal.Core.Data
{
    /// <summary>
    /// This class is a factory class that creates 
    /// data-base specific factories which in turn create data acces objects
    /// </summary>
    public class DatabaseFactory
    {

        // private static IServiceProvider Provider { get; set; }

        // public DatabaseFactory(IServiceProvider provider)
        // {
        //     Provider = provider;
        // }

        /// <summary>
        ///  gets a provider specific (i.e. database specific) factory 
        /// </summary>
        /// <param name="dialect"></param>
        /// <param name="serviceProvider"></param>
        /// <returns>an instance of service factory of given provider.</returns>
        public static IDatabaseFactory GetFactory(Dialect dialect, IServiceProvider serviceProvider)
        {
            // return the requested DbFactory
            var configuration = serviceProvider.GetService<IConfigurationRoot>();

            switch (dialect)
            {
                //instance of corresponding provider
                //case Dialect.MySQL:
                //    break;
                //case Dialect.PostgreSQL:
                //    break;
                case Dialect.SQLServer:
                    var dbfactory = new MsSQLFactory(configuration,serviceProvider);
                    DbFactoryProvider.SetCurrentDbFactory(dbfactory);
                    return DbFactoryProvider.GetFactory();
                //case Dialect.SQLite:
                //    break;

                default:
                    var dbFactory = new MsSQLFactory(configuration,serviceProvider);
                    DbFactoryProvider.SetCurrentDbFactory(dbFactory);
                    return DbFactoryProvider.GetFactory();
            }
        }

    }
}
