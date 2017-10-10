using VPortal.Core.Data.Crud;
using VExam.DTO;
using System.Threading.Tasks;
using VExam.DTO.ViewModel;
using System.Collections.Generic;

namespace VExam.Services.Answers
{
    public interface IAnswerService
    {
        CrudService<AnswersByInterviewees> CrudService { get; set; }
        Task<int> SaveAnswerAsync(IEnumerable<AnswersByInterviewees> Answer);
        // Task<QuestionViewModel> FilterAnswerAsync(int questionType);
        Task CheckObjectiveAnswers(long intervieweeId);
        Task<int> CheckAnswer(Result model);
        Task<IEnumerable<QuestionViewModel>> GetInterviewQuestionAnswerSheet(long intervieweeId);
    }
}