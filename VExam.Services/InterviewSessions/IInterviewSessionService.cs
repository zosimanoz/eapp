using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VExam.DTO;
using System.Threading.Tasks;
using VExam.DTO.ViewModel;
using System.Collections.Generic;

namespace VExam.Services.InterviewSessions
{
    public interface IInterviewSessionService
    {
        CrudService<InterviewSession> CrudService { get; set; }
        Task<long> DeleteAsync(long interviewSessionId);
        Task<IEnumerable<InterviewSession>> GetActiveInterviewSessions();
        Task<int> AddInterviewSessionAsync(InterviewSession model);
        Task<int> UpdateAsync(InterviewSession model);
    }
}