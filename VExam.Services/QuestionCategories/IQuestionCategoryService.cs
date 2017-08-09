using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VExam.DTO;
using System.Threading.Tasks;

namespace VExam.Services.QuestionCategories
{
    public interface IQuestionCategoryService
    {
         CrudService<QuestionCategory> CrudService { get; set; }
         Task<int> DeleteAsync(int questionCategoryId);
    }
}