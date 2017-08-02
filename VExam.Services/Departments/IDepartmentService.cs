using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VExam.DTO;

namespace VExam.Services.Departments
{
    public interface IDepartmentService
    {
         CrudService<Department> CrudService { get; set; }
    }
}