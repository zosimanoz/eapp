using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
        public async Task<int> SaveAnswerAsync(IEnumerable<AnswersByInterviewees> Answer)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    await db.OpenAsync();
                    using (IDbTransaction tran = db.BeginTransaction())
                    {
                        try
                        {
                            var result = 0;
                            foreach (var item in Answer)
                            {
                                string optionQuery = "INSERT INTO dbo.AnswersByInterviewees VALUES" +
                                " (@IntervieweeId, @SetQuestionId,@subjectiveAnswer,@ObjectiveAnswer,@AnsweredBy,@AuditTs,@Deleted,@IsChecked)";
                                result = await db.ExecuteAsync(optionQuery,
                                 new
                                 {
                                     IntervieweeId = item.IntervieweeId,
                                     SetQuestionId = item.SetQuestionId,
                                     subjectiveAnswer = item.subjectiveAnswer,
                                     ObjectiveAnswer = item.ObjectiveAnswer,
                                     AnsweredBy = item.AnsweredBy,
                                     AuditTs = DateTimeOffset.UtcNow,
                                     Deleted = 0,
                                     IsChecked = 0
                                 }, tran);
                            }
                            tran.Commit();
                            Console.WriteLine("result");
                            Console.WriteLine(result);
                            return result;
                        }
                        catch (Exception)
                        {
                            tran.Rollback();
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
        // public async Task<QuestionViewModel> FilterAnswerAsync(int questionType){

        // }

        public async Task CheckObjectiveAnswers(long intervieweeId)
        {
            try
            {
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {
                    DynamicParameters ObjParm = new DynamicParameters();
                    ObjParm.Add("@intervieweeId", intervieweeId);
                    db.Open();
                    await db.ExecuteAsync("sp_check_objective_answers", ObjParm, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> CheckAnswer(IEnumerable<Result> model)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    await db.OpenAsync();
                    foreach (var item in model)
                    {
                        if (item.ResultId > 0)
                        {
                            string checkAnswerQuery = "UPDATE dbo.Results SET " +
                            "AnswerId = @AnswerId, " +
                            "MarksObtained = @MarksObtained, " +
                            "Remarks = @Remarks, " +
                            "ExaminerId = @ExaminerId, " +
                            "AuditTs = @AuditTs "+
                            "WHERE ResultId = @ResultId";
                            var result = await db.ExecuteAsync(checkAnswerQuery,
                                    new
                                    {
                                        AnswerId = item.AnswerId,
                                        MarksObtained = item.MarksObtained,
                                        Remarks = item.Remarks,
                                        CheckedBy = item.CheckedBy,
                                        ExaminerId = item.ExaminerId,
                                        AuditTs = DateTimeOffset.UtcNow,
                                        Deleted = item.Deleted,
                                        ResultId=item.ResultId
                                    });
                        }
                        else
                        {
                            Console.WriteLine(item.AnswerId);
                            item.CheckedBy = 2;
                            string checkAnswerQuery = "INSERT INTO dbo.Results VALUES " +
                            "(@AnswerId,@MarksObtained,@Remarks,@CheckedBy,@ExaminerId,@AuditTs,@Deleted)";
                            var result = await db.ExecuteAsync(checkAnswerQuery,
                                    new
                                    {
                                        AnswerId = item.AnswerId,
                                        MarksObtained = item.MarksObtained,
                                        Remarks = item.Remarks,
                                        CheckedBy = item.CheckedBy,
                                        ExaminerId = item.ExaminerId,
                                        AuditTs = DateTimeOffset.UtcNow,
                                        Deleted = item.Deleted
                                    });

                            if (result > 0)
                            {
                                string updateAnswerByIntervieweeQuery = "UPDATE AnswersByInterviewees " +
                                "SET IsChecked = 1 WHERE AnswerId = @AnswerId";
                                await db.ExecuteAsync(updateAnswerByIntervieweeQuery,
                                        new
                                        {
                                            AnswerId = item.AnswerId
                                        });

                            }
                        }

                    }
                    return 1;

                }
                catch (Exception)
                {
                    throw;
                }

            }

        }

        public async Task<IEnumerable<QuestionViewModel>> GetInterviewQuestionAnswerSheet(long intervieweeId)
        {
            try
            {
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {
                    await db.OpenAsync();
                    List<QuestionViewModel> question = new List<QuestionViewModel>();
                    var query = "SELECT * FROM dbo.InterviewQuestions WHERE IntervieweeId = @IntervieweeId";
                    var result = await db.QueryAsync<QuestionBanks>(query, new
                    {
                        IntervieweeId = intervieweeId
                    });
                    foreach (var item in result)
                    {

                        // can also check if the question type is subjective or objective;
                        var optionsQuery = "SELECT * FROM dbo.ObjectiveQuestionOptions WHERE QuestionId = @QuestionId";

                        var options = await db.QueryAsync<ObjectiveQuestionOption>(optionsQuery, new
                        {
                            QuestionId = item.QuestionId
                        });
                        var answerQuery = "SELECT * FROM dbo.AnswersByInterviewees " +
                        "WHERE IntervieweeId = @IntervieweeId " +
                        "AND SetQuestionId = @SetQuestionId ";

                        var answer = await db.GetAsync<AnswersByInterviewees>(answerQuery, new
                        {
                            IntervieweeId = item.IntervieweeId,
                            SetQuestionId = item.SetQuestionId
                        });
                        Console.WriteLine(item.QuestionId);
                        Console.WriteLine(item.SetQuestionId);
                        var questionModel = new QuestionViewModel
                        {
                            Question = item,
                            Options = options,
                            Answers = answer
                        };
                        question.Add(questionModel);
                    }
                    return question;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<dynamic> GetInterviewAnswerSheetForExamineer(long intervieweeId)
        {
            try
            {
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {
                    await db.OpenAsync();
                    List<InterviewQuestionAnswersheetForExamineerViewModel> question = new List<InterviewQuestionAnswersheetForExamineerViewModel>();
                    var query = "SELECT * FROM dbo.InterviewQuestionsToCheckAnswersView WHERE IntervieweeId = @IntervieweeId";
                    var result = await db.QueryAsync<InterviewQuestionsForExamineer>(query, new
                    {
                        IntervieweeId = intervieweeId
                    });
                    foreach (var item in result)
                    {

                        // can also check if the question type is subjective or objective;
                        var optionsQuery = "SELECT * FROM dbo.ObjectiveOptionsWithAnswersByIntervieweesView WHERE QuestionId = @QuestionId";

                        var options = await db.QueryAsync<ObjectiveQuestionOption>(optionsQuery, new
                        {
                            QuestionId = item.QuestionId
                        });
                        var questionModel = new InterviewQuestionAnswersheetForExamineerViewModel
                        {
                            Question = item,
                            Options = options

                        };
                        question.Add(questionModel);
                    }
                    return question;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<dynamic> GetInterviewSubjectiveAnswerSheetForExamineer(long intervieweeId)
        {
            try
            {
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {
                    await db.OpenAsync();
                    var query = "SELECT * FROM dbo.InterviewQuestionsToCheckAnswersView WHERE IntervieweeId = @IntervieweeId AND QuestionTypeId = @QuestionTypeId";
                    var result = await db.QueryAsync<InterviewQuestionsForExamineer>(query, new
                    {
                        IntervieweeId = intervieweeId,
                        QuestionTypeId = 1
                    });

                    return result;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<dynamic> GetInterviewObjectiveAnswerSheetForExamineer(long intervieweeId)
        {
            try
            {
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {
                    await db.OpenAsync();
                    List<InterviewQuestionAnswersheetForExamineerViewModel> question = new List<InterviewQuestionAnswersheetForExamineerViewModel>();
                    var query = "SELECT * FROM dbo.InterviewQuestionsToCheckAnswersView WHERE IntervieweeId = @IntervieweeId AND QuestionTypeId = @QuestionTypeId";
                    var result = await db.QueryAsync<InterviewQuestionsForExamineer>(query, new
                    {
                        IntervieweeId = intervieweeId,
                        QuestionTypeId = 2
                    });
                    foreach (var item in result)
                    {

                        // can also check if the question type is subjective or objective;
                        var optionsQuery = "SELECT * FROM dbo.ObjectiveOptionsWithAnswersByIntervieweesView WHERE QuestionId = @QuestionId";

                        var options = await db.QueryAsync<ObjectiveQuestionOption>(optionsQuery, new
                        {
                            QuestionId = item.QuestionId
                        });
                        var questionModel = new InterviewQuestionAnswersheetForExamineerViewModel
                        {
                            Question = item,
                            Options = options

                        };
                        question.Add(questionModel);
                    }
                    return question;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}