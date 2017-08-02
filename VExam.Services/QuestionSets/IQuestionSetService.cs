using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VExam.DTO;
using System.Threading.Tasks;
using VExam.DTO.ViewModel;

namespace VExam.Services.QuestionSets
{
    public interface IQuestionSetService
    {
         CrudService<QuestionSet> CrudService { get; set; }
          Task<int> AddQuestionsAsync(SetQuestionViewModel model);
    }
}