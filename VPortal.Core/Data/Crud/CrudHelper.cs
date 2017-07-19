using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace VPortal.Core.Data.Crud
{
    public static class CRUDHelper
    {
        public static IDatabaseFactory GetFactory(this IDbConnection connection)
        {
            return DbFactoryProvider.GetFactory();
        }


        // crud builder will utilize the QueryBuilder class which return the QueryResponse
        // and that QueryResponse is utilized by other classes like CRUDHelper
        // here the factory functions are supplied with connection and transaction

        public static T Get<T>(this IDbConnection connection, object id, IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();

            var sql = factory.QueryBuilder.Get<T>(id);
            return connection.Query<T>(sql.QuerySql, sql.Parameters, transaction, true, commandTimeout).FirstOrDefault();
        }


        //public static T MyCustomTest<T>(this IDbConnection connection, object id, IDbTransaction transaction = null,
        //    int? commandTimeout = null)
        //{
        //    IDatabaseFactory factory = connection.GetFactory();

        //    var sql = factory.QueryBuilder.Get<T>(id);
        //    return connection.Query<T>(sql.QuerySql, sql.Parameters, transaction, true, commandTimeout).FirstOrDefault();
        //}


        public static async Task<T> GetAsync<T>(this IDbConnection connection, object id,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.Get<T>(id);
            var result = await connection.QueryAsync<T>(sql.QuerySql, sql.Parameters, transaction, commandTimeout);
            return result.FirstOrDefault();
        }

    }
}
