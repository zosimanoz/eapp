using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using VExam.DTO;
using VPortal.Core.Data;
using VPortal.Core.Data.Crud;

namespace VExam.Services.DAL
{
    public class Interviewees
    {

        public static async Task<bool> IntervieweeValidationAsync(string emailaddress, string contactnumber)
        {
            try
            {
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {


                    DynamicParameters ObjParm = new DynamicParameters();
                    ObjParm.Add("@emailaddress", emailaddress);
                    ObjParm.Add("@contactnumber", contactnumber);
                    ObjParm.Add("@Result", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    db.Open();
                    await db.ExecuteAsync("sp_validateInterviewee", ObjParm, commandType: CommandType.StoredProcedure);
                    var result = ObjParm.Get<bool>("@Result");
                    Console.WriteLine(result);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Interviewee> GetIntervieweeDetailAsync(string emailaddress, string contactnumber)
        {
            try
            {
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {

                    string questionQuery = "SELECT * FROM dbo.InterviewSessionCandidate_view" +
                                            "WHERE EmailAddress = @emailaddress" +
                                            "AND ContactNumber = @ContactNumber";
                    var result = await db.GetAsync<Interviewee>(questionQuery,
                    new
                    {
                        emailaddress = emailaddress,
                        ContactNumber = contactnumber
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