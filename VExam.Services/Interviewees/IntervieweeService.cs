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

namespace VExam.Services.Interviewees
{
    public class IntervieweeService : IIntervieweeService
    {
        public CrudService<Interviewee> CrudService { get; set; } = new CrudService<Interviewee>();

        public async Task<bool> IntervieweeValidationAsync(string emailaddress, string contactnumber)
        {
            try
            {
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {
                    db.Open();
                    var query = "SELECT * FROM dbo.InterviewSessionCandidate_view WHERE EmailAddress = @EmailAddress" +
                    "AND ContactNumber = @ContactNumber";

                    var result = await db.GetAsync<Interviewee>(query, new
                    {
                        EmailAddress = emailaddress,
                        ContactNumber = contactnumber
                    });
                    if(result !=null){
                        return true;
                    }else{
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}