using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

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


        // gets one record based on the primary key
        public static T Get<T>(this IDbConnection connection, object id, IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.Get<T>(id);
            return connection.Query<T>(sql.QuerySql, sql.Parameters, transaction, true, commandTimeout).FirstOrDefault();
        }


        // gets one record based on the primary key async
        public static async Task<T> GetAsync<T>(this IDbConnection connection, object id,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.Get<T>(id);
            var result = await connection.QueryAsync<T>(sql.QuerySql, sql.Parameters, transaction, commandTimeout);
            return result.FirstOrDefault();
        }


        // get one record based on condition and paramters
        public static T Get<T>(this IDbConnection connection, string condition, object parameters,
          IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.Get<T>(condition, parameters);
            var result = connection.Query<T>(sql.QuerySql, sql.Parameters);
            return result.FirstOrDefault();
        }



        // get one record based on condition and paramters async
        public static async Task<T> GetAsync<T>(this IDbConnection connection, string condition, object parameters,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.Get<T>(condition,parameters);
            var result = await connection.QueryAsync<T>(sql.QuerySql, sql.Parameters, transaction, commandTimeout);
            return result.FirstOrDefault();
        }


        // gets list of records all records from a table
        public static T GetList<T>(this IDbConnection connection, object whereConditions,
          IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.GetList<T>(whereConditions);
            return connection.Query<T>(sql.QuerySql, sql.Parameters, transaction, true, commandTimeout);
        }



        // gets list of records all records from a table async
        public static Task<IEnumerable<T>> GetListAsync<T>(this IDbConnection connection, object whereConditions,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.GetList<T>(whereConditions);
            return connection.QueryAsync<T>(sql.QuerySql, sql.Parameters, transaction, commandTimeout);
        }


        // get list based on conditions and parameters
        public static IEnumerable<T> GetList<T>(this IDbConnection connection, string conditions,
            object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.GetList<T>(conditions, parameters);
            return connection.Query<T>(sql.QuerySql, sql.Parameters, transaction, true, commandTimeout);
        }



        // get list based on conditions and parameters async
        public static IEnumerable<T> GetListAsync<T>(this IDbConnection connection, string conditions,
            object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.GetList<T>(conditions, parameters);
            return connection.QueryAsync<T>(sql.QuerySql, sql.Parameters, transaction, true, commandTimeout);
        }


        // get list of model T
        public static IEnumerable<T> GetList<T>(this IDbConnection connection)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.GetList<T>(new { });
            return connection.Query<T>(sql.QuerySql);
        }


        // get list of model T
        public static Task<IEnumerable<T>> GetListAsync<T>(this IDbConnection connection)
        {
            return connection.GetListAsync<T>(new { });
        }

        // gets paged list of all records matching the conditions
         public static IEnumerable<T> GetListPaged<T>(this IDbConnection connection, int pageNumber, int rowsPerPage,
            int pageSize, string conditions, string orderby, object parameters = null, IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.GetPaginatedList<T>(pageNumber, rowsPerPage, conditions, orderby, parameters);
            return connection.Query<T>(sql.QuerySql, sql.Parameters, transaction, true, commandTimeout);
        }

        // gets paged list of all records matching the conditions asynchronously
        public static Task<IEnumerable<T>> GetListPagedAsync<T>(this IDbConnection connection, int pageNumber, int rowsPerPage,
            int pageSize, string conditions, string orderby, object parameters = null, IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.GetPaginatedList<T>(pageNumber, rowsPerPage, conditions, orderby, parameters);
            return connection.QueryAsync<T>(sql.QuerySql, sql.Parameters, transaction, commandTimeout);

        }

        // Inserts a record and returns the new primary key
        public static int? Insert(this IDbConnection connection, object entityToInsert,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Insert<int?>(connection, entityToInsert, transaction, commandTimeout);
        }

        // Inserts a record and returns the new primary key
        public static Task<int?> InsertAsync(this IDbConnection connection, object entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return InsertAsync<int?>(connection, entityToInsert, transaction, commandTimeout);
        }


        // Inserts a record and returns the new primary key based on dynamic Model
        public static TKey Insert<TKey>(this IDbConnection connection, object entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.Insert<TKey>(entityToInsert);
            var result = connection.Query(sql.QuerySql, sql.Parameters, transaction, true, commandTimeout);
            if (sql.IsKeyGuidType || sql.KeyHasPredefinedValue)
            {
                return (TKey)sql.Id;
            }
            return (TKey)result.First().id;
        }


        // Inserts a record and returns the new primary key based on dynamic Model async
        public static async Task<TKey> InsertAsync<TKey>(this IDbConnection connection, object entityToInsert,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.Insert<TKey>(entityToInsert);
            var result = await connection.QueryAsync(sql.QuerySql, sql.Parameters, transaction, commandTimeout);
            if (sql.IsKeyGuidType || sql.KeyHasPredefinedValue)
            {
                return (TKey)sql.Id;
            }
            return (TKey)result.First().id;
        }


        // Updates a record
        public static int Update(this IDbConnection connection, object entityToUpdate, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.Update(entityToUpdate);
            return connection.Execute(sql.QuerySql, sql.Parameters, transaction, commandTimeout);
        }


        // Updates a record async
         public static Task<int> UpdateAsync(this IDbConnection connection, object entityToUpdate,
            IDbTransaction transaction = null, int? commandTimeout = null,
            System.Threading.CancellationToken? token = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.Update(entityToUpdate);
            System.Threading.CancellationToken cancelToken = token ?? default(System.Threading.CancellationToken);
            return connection.ExecuteAsync(new CommandDefinition(sql.QuerySql, sql.Parameters, transaction, commandTimeout, cancellationToken: cancelToken));

        }


        // Updates a record using condition and parameters
        public static int Update(this IDbConnection connection, object entityToUpdate,
          string condition, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null,
           System.Threading.CancellationToken? token = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.Update(entityToUpdate, condition, parameters);
            System.Threading.CancellationToken cancelToken = token ?? default(System.Threading.CancellationToken);
            return connection.Execute(new CommandDefinition(sql.QuerySql, sql.Parameters, transaction, commandTimeout, cancellationToken: cancelToken));

        }

        // Updates a record using condition and parameters
        public static Task<int> UpdateAsync(this IDbConnection connection, object entityToUpdate,
          string condition, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null,
           System.Threading.CancellationToken? token = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.Update(entityToUpdate, condition, parameters);
            System.Threading.CancellationToken cancelToken = token ?? default(System.Threading.CancellationToken);
            return connection.ExecuteAsync(new CommandDefinition(sql.QuerySql, sql.Parameters, transaction, commandTimeout, cancellationToken: cancelToken));

        }

        
        
        // Deletes a record based on the typed entity
        public static int Delete<T>(this IDbConnection connection, T entityToDelete, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.Delete<T>(entityToDelete);
            return connection.Execute(sql.QuerySql, sql.Parameters, transaction, commandTimeout);
        }


        // Deletes a record based on the typed entity async
        public static Task<int> DeleteAsync<T>(this IDbConnection connection, T entityToDelete,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.Delete<T>(entityToDelete);
            return connection.ExecuteAsync(sql.QuerySql, sql.Parameters, transaction, commandTimeout);

        }



        // Delete entity based on condition
        public static int Delete<T>(this IDbConnection connection, string condition, object parameters = null,
           IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.Delete<T>(condition, parameters);
            return connection.Execute(sql.QuerySql, sql.Parameters, transaction, commandTimeout);

        }


        // Delete entity based on condition async
        public static Task<int> DeleteAsync<T>(this IDbConnection connection, string condition, object parameters = null,
           IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.Delete<T>(condition, parameters);
            return connection.ExecuteAsync(sql.QuerySql, sql.Parameters, transaction, commandTimeout);

        }


        // Delete based on id/primary key
        public static int Delete<T>(this IDbConnection connection, object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.Delete<T>(id);
            return connection.Execute(sql.QuerySql, sql.Parameters, transaction, commandTimeout);
        }


        // Delete based on id/primary key async
        public static Task<int> DeleteAsync<T>(this IDbConnection connection, object id,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.Delete<T>(id);
            return connection.ExecuteAsync(sql.QuerySql, sql.Parameters, transaction, commandTimeout);

        }


        // Batch delete based on primary key/id
        public static int Delete<T>(this IDbConnection connection, int[] ids,
           IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.Delete<T>(ids);
            return connection.Execute(sql.QuerySql, sql.Parameters, transaction, commandTimeout);

        }

        // Batch delete based on primary key/id async
        public static Task<int> DeleteAsync<T>(this IDbConnection connection, int[] ids,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.Delete<T>(ids);
            return connection.ExecuteAsync(sql.QuerySql, sql.Parameters, transaction, commandTimeout);

        }

        // (anonymous object for where clause) - deletes all records matching the where options
        public static int DeleteList<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.DeleteList<T>(whereConditions);
            return connection.Execute(sql.QuerySql, sql.Parameters, transaction, commandTimeout);
        }

        // (string for conditions, anonymous object with parameters) - deletes list of all records matching the conditions
        public static int DeleteList<T>(this IDbConnection connection, string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.DeleteList<T>(conditions, parameters);
            return connection.Execute(sql.QuerySql, sql.Parameters, transaction, commandTimeout);
        }


        // (anonymous object for where clause) - deletes all records matching the where options async
        public static Task<int> DeleteListAsync<T>(this IDbConnection connection, object whereConditions,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.DeleteList<T>(whereConditions);
            return connection.ExecuteAsync(sql.QuerySql, sql.Parameters, transaction, commandTimeout);
        }


        // (anonymous object for where clause) - deletes all records matching the string conditions async
         public static Task<int> DeleteListAsync<T>(this IDbConnection connection, string conditions,
            object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.DeleteList<T>(conditions, parameters);
            return connection.ExecuteAsync(sql.QuerySql, sql.Parameters, transaction, commandTimeout);

        }

        // (string for conditions,anonymous object with parameters) -gets count of all records matching the conditions
         public static int RecordCount<T>(this IDbConnection connection, string conditions = "", object parameters = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.RecordCount<T>(conditions, parameters);
            return connection.ExecuteScalar<int>(sql.QuerySql, sql.Parameters, transaction, commandTimeout);
        }


        // (string for conditions,anonymous object with parameters) -gets count of all records where condition
        public static int RecordCount<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.RecordCount<T>(whereConditions);
            return connection.ExecuteScalar<int>(sql.QuerySql, sql.Parameters, transaction, commandTimeout);
        }


        // (string for conditions,anonymous object with parameters) -gets count of all records matching the conditions async
         public static Task<int> RecordCountAsync<T>(this IDbConnection connection, string conditions = "", object parameters = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.RecordCount<T>(conditions, parameters);
            return connection.ExecuteScalarAsync<int>(sql.QuerySql, sql.Parameters, transaction, commandTimeout);
        }


        // (string for conditions,anonymous object with parameters) -gets count of all records where condition async
        public static Task<int> RecordCountAsync<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDatabaseFactory factory = connection.GetFactory();
            var sql = factory.QueryBuilder.RecordCount<T>(whereConditions);
            return connection.ExecuteScalarAsync<int>(sql.QuerySql, sql.Parameters, transaction, commandTimeout);
        }

    }
}
