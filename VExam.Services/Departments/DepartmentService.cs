using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VPortal.Core.Data;
using System.Text;
using System;
using VExam.DTO;
using System.Threading.Tasks;

namespace VExam.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        public CrudService<Department> CrudService { get; set; } = new CrudService<Department>();

        public async Task<int> DeleteAsync(int departmentId)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    db.Open();
                    string questionQuery = "UPDATE dbo.Departments SET deleted = @delete WHERE DepartmentId = @departmentId";
                    var result = await db.ExecuteAsync(questionQuery,
                            new
                            {
                                delete = 1,
                                departmentId = departmentId
                            });
                    return result;

                }
                catch (Exception)
                {
                    throw;
                }

            }

        }
    }
}