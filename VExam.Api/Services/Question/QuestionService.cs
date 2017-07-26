using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VPortal.Core.Data;
using System.Text;
using System;
using VExam.Api.DTO;

namespace VExam.Api.Services.Question
{
    public class QuestionService:IQuestionService
    {
         public CrudService<Questions> CrudService { get; set; } = new CrudService<Questions>();
    }
}