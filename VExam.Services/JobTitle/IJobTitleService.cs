using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VExam.DTO;

namespace VExam.Services.JobTitle
{
    public interface IJobTitleService
    {
         CrudService<JobTitles> CrudService { get; set; }
    }
}