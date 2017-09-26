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

        public async Task<IEnumerable<InterviewSession>> GetInterviewSessionHistory()
        {
            try
            {
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {
                    await db.OpenAsync();
                    List<QuestionViewModel> question = new List<QuestionViewModel>();
                    var query = "SELECT * FROM dbo.InterviewSessionHistoryView";
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
                    Console.WriteLine(model.CreatedBy);
                    await db.OpenAsync();
                    string optionQuery = "INSERT INTO dbo.InterviewSessions VALUES" +
                    " (@Title, @StartDate,@EndDate,@CreatedBy,@AuditTs,@Deleted)";
                    var result = await db.ExecuteAsync(optionQuery,
                      new
                      {
                          Title = model.Title,
                          StartDate = model.SessionStartDate,
                          EndDate = model.SessionEndDate,
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
                    "Title = @Title," +
                    "SessionStartDate = @StartDate," +
                    "SessionEndDate = @EndDate," +
                    "AuditTs = @AuditTs " +
                    "WHERE InterviewSessionId = @IntervierwSessionId";
                    var result = await db.ExecuteAsync(optionQuery,
                      new
                      {
                          Title = model.Title,
                          StartDate = model.SessionStartDate,
                          EndDate = model.SessionEndDate,
                          AuditTs = DateTime.Now,
                          IntervierwSessionId = model.InterviewSessionId
                      });
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<IEnumerable<ResultSummary>> ResultSummaryAsync(ResultSummary model)
        {
            try
            {
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {
                    await db.OpenAsync();
                    string questionQuery = "SELECT * FROM dbo.InterviewResultSummaryView WHERE " +
                                             "(@IntervieweeId IS NULL OR IntervieweeId = @IntervieweeId) " +
                                             "AND (@InterviewSessionId IS NULL OR InterviewSessionId = @InterviewSessionId) " +
                                             "AND (EmailAddress LIKE @EmailAddress) " +
                                             "AND (IntervieweeName LIKE @IntervieweeName)" +
                                             " ORDER BY MarksObtained DESC";
                    var result = await db.QueryAsync<ResultSummary>(questionQuery,
                    new
                    {
                        IntervieweeId = model.IntervieweeId == 0 ? (int?)null : (int?)model.IntervieweeId,
                        InterviewSessionId = model.InterviewSessionId == 0 ? (int?)null : (int?)model.InterviewSessionId,
                        EmailAddress = "%" + model.EmailAddress + "%",
                        IntervieweeName = "%" + model.IntervieweeName + "%"
                    });
                    Console.WriteLine(questionQuery);

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