using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VPortal.Core.Data;
using System.Text;
using System;
using VExam.Api.DTO;

namespace VExam.Api.Services.QuestionComplexities
{
    public class QuestionComplexityService:IQuestionComplexityService
    {
         public CrudService<QuestionComplexity> CrudService { get; set; } = new CrudService<QuestionComplexity>();
    }
}