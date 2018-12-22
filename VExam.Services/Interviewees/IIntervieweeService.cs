using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VExam.DTO;
using System.Threading.Tasks;
using VExam.DTO.ViewModel;
using System.Collections.Generic;

namespace VExam.Services.Interviewees
{
    public interface IIntervieweeService
    {
        CrudService<Interviewee> CrudService { get; set; }
        Task<IEnumerable<Interviewee>> GetintervieweesBySessionIdAsync(long interviewSessionId);
        Task<IEnumerable<Interviewee>> GetExamAttendedintervieweesBySessionIdAsync(long interviewSessionId);
        Task<bool> IntervieweeValidationAsync(string emailaddress, string contactnumber);
        Task<long> DeleteIntervieweeAsync(long intervieweeId);
        Task<IEnumerable<QuestionViewModel>> GetinterviewQuestions(long intervieweeId);
        Task<Interviewee> GetIntervieweeDetailById(long intervieweeId);
    }
}