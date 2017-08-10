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

namespace VExam.Services.SessionwiseJobs
{
    public class SessionwiseJobsService : ISessionwiseJobsService
    {
        public CrudService<SessionwiseJob> CrudService { get; set; } = new CrudService<SessionwiseJob>();
        public async Task<int> DeleteAsync(long sessionwiseJobId)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    await db.OpenAsync();
                    string questionQuery = "UPDATE dbo.SessionwiseJobs SET deleted = @delete WHERE SessionwiseJobId = @SetQuestionId";
                    var result = await db.ExecuteAsync(questionQuery,
                            new
                            {
                                delete = 1,
                                SessionwiseJobId = sessionwiseJobId
                            });
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