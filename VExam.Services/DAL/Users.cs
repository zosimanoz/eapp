using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using VExam.DTO;
using VPortal.Core.Data;
using VPortal.Core.Data.Crud;

namespace VExam.Services.DAL
{
    public static class Users
    {
         public static async Task<string> GetUserPasswordAsync(string emailAddress)
        {

            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    db.Open();
                    string questionQuery = "SELECT password FROM dbo.Users " +
                    "WHERE EmailAddress = @EmailAddress";
                    var result = await db.GetAsync<string>(questionQuery,
                            new
                            {
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
        public static async Task<User> GetUserDetailAsync(string emailaddress)
        {
            try
            {
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {
                    string questionQuery = "SELECT * FROM dbo.Users " +
                                            "WHERE EmailAddress = @emailaddress ";
                    var result = await db.GetAsync<User>(questionQuery,
                    new
                    {
                        emailaddress = emailaddress
                    });
                  
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}