using System.Threading.Tasks;
using VPortal.Core.Data.Crud;
using VExam.DTO;

namespace VExam.Services.ExamSetService
{
    public interface IExamSetService
    {
        CrudService<ExamSet> CrudService { get; set; }
        Task<int> DeleteExamSetAsync(long examSetId);
    }
}