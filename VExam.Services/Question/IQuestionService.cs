using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VExam.DTO;
using VExam.DTO.ViewModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace VExam.Services.Question
{
    public interface IQuestionService
    {
        CrudService<QuestionBanks> CrudService { get; set; }
        CrudService<ObjectiveQuestionOption> ObjectiveOptionCrudService { get; set; }
        Task<int> AddQuestionAsync(QuestionViewModel model);
        Task<int> DeleteQuestionAsync(long questionId);
       Task<IEnumerable<QuestionBanks>> SearchQuestionAsync(QuestionSearch model);
    }
}