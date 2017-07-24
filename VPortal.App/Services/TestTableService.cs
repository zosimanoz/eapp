using VPortal.App.Controllers;
using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VPortal.Core.Data;
using System.Text;
using System;

namespace VPortal.App.Services
{
    public class TestTableService : ITestTableService
    {
        public CrudService<TestTable> CrudService { get; set; } = new CrudService<TestTable>();

        public void Add(string name)
        {
            try
            {
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {
                    db.Open();
                    var data = db.Query("select * from [dbo].[TestTable]");   

                    Console.Write(data);                 
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}