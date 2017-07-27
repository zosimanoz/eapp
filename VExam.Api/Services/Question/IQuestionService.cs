using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VExam.Api.DTO;
using VExam.Api.ViewModel;
using System.Threading.Tasks;

namespace VExam.Api.Services.Question
{
    public interface IQuestionService
    {
        CrudService<Questions> CrudService { get; set; }
        CrudService<ObjectiveQuestionOption> ObjectiveOptionCrudService { get; set; }
        Task<int> AddQuestionAsync(QuestionViewModel model);
        Task<int> DeleteQuestionAsync(long questionId);
    }
}