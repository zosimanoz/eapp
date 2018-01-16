using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VPortal.Core.Data;
using System.Text;
using System;
using VExam.DTO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace VExam.Services.Users
{
    public class UserService : IUserService
    {
        public CrudService<User> CrudService { get; set; } = new CrudService<User>();
        public async Task<int> DeleteAsync(long UserId)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    await db.OpenAsync();
                    string questionQuery = "UPDATE dbo.Users SET deleted = @delete WHERE UserId = @UserId";
                    var result = await db.ExecuteAsync(questionQuery,
                            new
                            {
                                delete = 1,
                                UserId = UserId
                            });
                    return result;

                }
                catch (Exception)
                {
                    throw;
                }

            }

        }
        public async Task<int> UpdatePasswordAsync(Password model)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    await db.OpenAsync();
                    string questionQuery = "UPDATE dbo.Users SET " +
                    "Password = @Password," +
                    "PasswordChanged = @PasswordChanged " +
                    "WHERE EmailAddress = @EmailAddress";
                    var result = await db.ExecuteAsync(questionQuery,
                            new
                            {
                                Password = model.NewPassword,
                                PasswordChanged = 1,
                                EmailAddress = model.EmailAddress
                            });
                    return result;

                }
                catch (Exception)
                {
                    throw;
                }

            }

        }


        public async Task<string> GetUserPasswordAsync(string emailAddress)
        {

            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    await db.OpenAsync();
                    string questionQuery = "SELECT password FROM dbo.Users " +
                    "WHERE EmailAddress = @EmailAddress";
                    var result = await db.QueryAsync<string>(questionQuery,
                            new
                            {
                                EmailAddress = emailAddress
                            });
                    return result.FirstOrDefault();

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<int> ResetPasswordAsync(string emailAddress, string password)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    await db.OpenAsync();
                    string questionQuery = "UPDATE dbo.Users SET " +
                    "Password = @Password," +
                    "PasswordChanged = @PasswordChanged " +
                    "WHERE EmailAddress = @EmailAddress";
                    var result = await db.ExecuteAsync(questionQuery,
                            new
                            {
                                Password = password,
                                PasswordChanged = 0,
                                EmailAddress = emailAddress
                            });
                    return result;

                }
                catch (Exception)
                {
                    throw;
                }

            }
        }
        public async Task<IEnumerable<User>> GetAllActiveUsersAsync()
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    await db.OpenAsync();
                    string questionQuery = "SELECT * FROM UsersView";
                    var result = await db.QueryAsync<User>(questionQuery);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

    }
}