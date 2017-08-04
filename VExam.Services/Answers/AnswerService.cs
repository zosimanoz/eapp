using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using VExam.DTO;
using VExam.DTO.ViewModel;
using VPortal.Core.Data;
using VPortal.Core.Data.Crud;

namespace VExam.Services.Answers
{
    public class AnswerService : IAnswerService
    {
     public CrudService<AnswersByInterviewees> CrudService { get; set; } = new CrudService<AnswersByInterviewees>();
         public async Task<int> SaveAnswerAsync(AnswersViewModel model)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    db.Open();
                    using (IDbTransaction tran = db.BeginTransaction())
                    {
                        try
                        {
                            var result =0;
                            foreach (var item in model.Answer)
                            {
                                string optionQuery = "INSERT INTO dbo.AnswersByInterviewees VALUES"+
                                " (@IntervieweeId, @SetQuestionId,@subjectiveAnswer,@ObjectiveAnswer,@AnsweredBy,@AuditTs,@Deleted)";
                               result= await db.ExecuteAsync(optionQuery,
                                new
                                {
                                    IntervieweeId      =item.IntervieweeId,
                                    SetQuestionId      = item.SetQuestionId,						
                                    subjectiveAnswer   = item.subjectiveAnswer,				
                                    ObjectiveAnswer	   = item.ObjectiveAnswer,				
                                    AnsweredBy	       =item.AnsweredBy,					
                                    AuditTs		       =DateTimeOffset.Now,		 				
                                    Deleted	           =0						
                                }, tran);
                            }
                            tran.Commit();
                            return result;
                        }
                        catch (Exception)
                        {
                            throw;
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
}