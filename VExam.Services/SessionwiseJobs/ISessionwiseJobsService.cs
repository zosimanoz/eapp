using VPortal.Core.Data.Crud;
using VExam.DTO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace VExam.Services.SessionwiseJobs
{
    public interface ISessionwiseJobsService
    {
        CrudService<SessionwiseJob> CrudService { get; set; }
        Task<int> DeleteAsync(long sessionwiseJobId);
        Task<IEnumerable<SessionwiseJob>> GetJobsBySessionIdAsync(long sessionId);
    }
}