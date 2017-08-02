using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VPortal.Core.Data;
using System.Text;
using System;
using VExam.DTO;

namespace VExam.Services.QuestionCategories
{
    public class QuestionCategoryService:IQuestionCategoryService
    {
         public CrudService<QuestionCategory> CrudService { get; set; } = new CrudService<QuestionCategory>();

    }
}