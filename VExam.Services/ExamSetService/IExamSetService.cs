using System.Threading.Tasks;
using VPortal.Core.Data.Crud;
using VExam.DTO;
using System.Collections.Generic;

namespace VExam.Services.ExamSetService
{
    public interface IExamSetService
    {
        CrudService<ExamSet> CrudService { get; set; }
        Task<int> DeleteExamSetAsync(long examSetId);
        Task<IEnumerable<ExamSet>> GetExamSetsByJobTitleAsync(long jobTitleId);
    }
}