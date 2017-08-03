using VPortal.Core.Data.Crud;
using VExam.DTO;
using System.Threading.Tasks;

namespace VExam.Services.SessionwiseJobs
{
    public interface ISessionwiseJobsService
    {
         CrudService<SessionwiseJob> CrudService { get; set; }
         Task<int> DeleteAsync(long sessionwiseJobId);
    }
}