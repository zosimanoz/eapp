using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VExam.DTO;
using System.Threading.Tasks;

namespace VExam.Services.Interviewees
{
    public interface IIntervieweeService
    {
         CrudService<Interviewee> CrudService { get; set; }

         Task<bool> IntervieweeValidationAsync(string emailaddress, string contactnumber);
    }
}