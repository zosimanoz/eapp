using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VExam.Api.DTO;

namespace VExam.Api.Services.JobTitle
{
    public interface IJobTitleService
    {
         CrudService<JobTitles> CrudService { get; set; }
    }
}