using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VExam.Api.DTO;

namespace VExam.Api.Services.QuestionComplexities
{
    public interface IQuestionComplexityService
    {
         CrudService<QuestionComplexity> CrudService { get; set; }
    }
}