using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VExam.DTO;
using System.Threading.Tasks;

namespace VExam.Services.JobTitle
{
    public interface IJobTitleService
    {
         CrudService<JobTitles> CrudService { get; set; }
         Task<int> DeleteAsync(int jobTitleId);
    }
}