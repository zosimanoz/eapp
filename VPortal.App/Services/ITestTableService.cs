using VPortal.App.Controllers;
using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;


namespace VPortal.App.Services
{
    public interface ITestTableService
    {
         CrudService<TestTable> CrudService { get; set; }

         void Add(string name);
    }
}