using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPortal.Core.Data.Crud
{
    public class CrudService<T>
    {

        private IDatabaseFactory DbFactory { get; }

        public CrudService(IDatabaseFactory dbFactory)
        {
            DbFactory = dbFactory;
        }


        public CrudService()
        {
            DbFactory = DbFactoryProvider.GetFactory();
        }


        public virtual T Query(string sql, object param = null)
        {
            using (var db = (DbConnection)DbFactory.GetConnection())
            {
                db.Open();
                var result = db.Query<T>(sql, param);
                return result.FirstOrDefault();
            }
        }


        public virtual async Task<T> QueryAsync(string sql, object param = null)
        {
            using (var db = (DbConnection)DbFactory.GetConnection())
            {
                await db.OpenAsync();
                var result = await db.QueryAsync<T>(sql, param);
                return result.FirstOrDefault();

            }
        }
        public virtual IEnumerable<T> QueryList(string sql, object param = null)
        {
            using (var db = (DbConnection)DbFactory.GetConnection())
            {
                db.Open();
                return db.Query<T>(sql, param);
            }
        }
        public virtual async Task<IEnumerable<T>> QueryListAsync(string sql, object param = null)
        {
            using (var db = (DbConnection)DbFactory.GetConnection())
            {
                await db.OpenAsync();
                return await db.QueryAsync<T>(sql, param);

            }
        }


        public virtual T Get(object id)
        {
            using (var db = (DbConnection)DbFactory.GetConnection())
            {
                db.Open();
                return db.Get<T>(id);
            }
        }


        public virtual async Task<T> GetAsync(object id)
        {
            using (var db = (DbConnection)DbFactory.GetConnection())
            {
                await db.OpenAsync();
                return await db.GetAsync<T>(id);
            }
        }


    }
}
