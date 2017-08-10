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
using VExam.DTO.ViewModel;
using System.Collections.Generic;

namespace VExam.Services.InterviewSessions
{
    public class InterviewSessionService : IInterviewSessionService
    {
        public CrudService<InterviewSession> CrudService { get; set; } = new CrudService<InterviewSession>();

        public async Task<long> DeleteAsync(long interviewSessionId)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    await db.OpenAsync();
                    string questionQuery = "UPDATE dbo.InterviewSessions SET deleted = @delete WHERE InterviewSessionId = @interviewSessionId";
                    var result = await db.ExecuteAsync(questionQuery,
                            new
                            {
                                delete = 1,
                                interviewSessionId = interviewSessionId
                            });
                    return result;

                }
                catch (Exception)
                {
                    throw;
                }

            }

        }
        public async Task<IEnumerable<InterviewSession>> GetActiveInterviewSessions()
        {
            try
            {
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {
                    await db.OpenAsync();
                    List<QuestionViewModel> question = new List<QuestionViewModel>();
                    var query = "SELECT * FROM dbo.ActiveInterviewSessionView";
                    var result = await db.QueryAsync<InterviewSession>(query);
                    return result;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> AddInterviewSessionAsync(InterviewSession model)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    await db.OpenAsync();
                    string optionQuery = "INSERT INTO dbo.InterviewSessions VALUES" +
                    " (@Title, @StartDate,@EndDate,@JobTitleId,@CreatedBy,@AuditTs,@Deleted)";
                    var result = await db.ExecuteAsync(optionQuery,
                      new
                      {
                          Title = model.Title,
                          StartDate = model.SessionStartDate,
                          EndDate = model.SessionEndDate,
                          JobTitleId=model.JobTitleId,
                          CreatedBy = model.CreatedBy,
                          AuditTs = DateTime.Now,
                          Deleted = 0
                      });
                      return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<int> UpdateAsync(InterviewSession model)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    await db.OpenAsync();
                    string optionQuery = "UPDATE dbo.InterviewSessions SET " +
                    "Title = @Title,"+
                    "SessionStartDate = @StartDate,"+
                    "SessionEndDate = @EndDate,"+
                    "JobTitleId = @JobTitleId,"+
                    "AuditTs = @AuditTs "+
                    "WHERE InterviewSessionId = @IntervierwSessionId";
                    var result = await db.ExecuteAsync(optionQuery,
                      new
                      {
                          Title = model.Title,
                          StartDate = model.SessionStartDate,
                          EndDate = model.SessionEndDate,
                          JobTitleId=model.JobTitleId,
                          AuditTs = DateTime.Now,
                          IntervierwSessionId=model.InterviewSessionId
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