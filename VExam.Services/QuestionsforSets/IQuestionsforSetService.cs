using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VExam.DTO;
using System.Threading.Tasks;
using VExam.DTO.ViewModel;
using System.Collections.Generic;

namespace VExam.Services.QuestionsforSets
{
    public interface IQuestionsforSetService
    {
        CrudService<SetQuestion> CrudService { get; set; }
        Task<int> DeleteQuestionAsync(long setQuestionId);
        Task<int> AddQuestionsAsync(SetQuestionViewModel model);
        Task<IEnumerable<SetQuestion>> GetQuestionsBySetIdAsync(long examSetId);
    }
}