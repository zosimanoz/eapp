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
using System.Collections.Generic;
using System.Linq;

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
                    string questionQuery = "UPDATE dbo.SessionwiseJobs SET deleted = @delete WHERE SessionwiseJobId = @SessionwiseJobId";
                    var result = await db.ExecuteAsync(questionQuery,
                            new
                            {
                                delete = 1,
                                SessionwiseJobId = sessionwiseJobId
                            });
                    Console.WriteLine(result);
                    return result;

                }
                catch (Exception)
                {
                    throw;
                }

            }

        }
        public async Task<IEnumerable<SessionwiseJob>> GetJobsBySessionIdAsync(long sessionId)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    await db.OpenAsync();
                    string query = "SELECT * FROM SessionJobView WHERE InterviewSessionId = @sessionId";
                    var result = await db.QueryAsync<SessionwiseJob>(query,
                            new
                            {
                                sessionId = sessionId
                            });
                    return result;

                }
                catch (Exception)
                {
                    throw;
                }

            }
        }

        public async Task<bool> CheckJobExistsInSessionAsync(long sessionId, int jobTitleId, long examSetId)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    await db.OpenAsync();
                    string query = "SELECT * FROM dbo.SessionwiseJobs WHERE InterviewSessionId = @sessionId " +
                    " AND JobTitleId = @jobTitleId AND ExamSetId = @examSetId";
                    var result = await db.QueryAsync<SessionwiseJob>(query,
                            new
                            {
                                sessionId = sessionId,
                                jobTitleId = jobTitleId,
                                examSetId = examSetId
                            });
                    bool exists = result.Any();
                    Console.WriteLine(exists);
                    return exists;
                }
                catch (Exception)
                {
                    throw;
                }

            }

        }

    }
}