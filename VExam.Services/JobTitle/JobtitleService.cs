using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VPortal.Core.Data;
using System.Text;
using System;
using VExam.DTO;

namespace VExam.Services.JobTitle
{
    public class JobTitleService:IJobTitleService
    {
         public CrudService<JobTitles> CrudService { get; set; } = new CrudService<JobTitles>();
    }
}