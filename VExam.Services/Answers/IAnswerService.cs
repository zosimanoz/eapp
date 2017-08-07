using VPortal.Core.Data.Crud;
using VExam.DTO;
using System.Threading.Tasks;
using VExam.DTO.ViewModel;

namespace VExam.Services.Answers
{
    public interface IAnswerService
    {
        CrudService<AnswersByInterviewees> CrudService { get; set; }
        Task<int> SaveAnswerAsync(AnswersViewModel model);
       // Task<QuestionViewModel> FilterAnswerAsync(int questionType);
    }
}