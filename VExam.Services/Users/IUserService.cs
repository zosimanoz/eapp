using VPortal.Core.Data.Crud;
using VExam.DTO;
using System.Threading.Tasks;

namespace VExam.Services.Users
{
    public interface IUserService
    {
        CrudService<User> CrudService { get; set; }
        Task<int> DeleteAsync(long UserId);
        Task<int> UpdatePasswordAsync(Password model);
        Task<string> GetUserPasswordAsync(string emailAddress);
        Task<int> ResetPasswordAsync(string emailAddress, string password);
    }
}