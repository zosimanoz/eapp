using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VPortal.Core.Data;
using System.Text;
using System;
using VExam.Api.DTO;
using VExam.Api.ViewModel;
using System.Threading.Tasks;

namespace VExam.Api.Services.Question
{
    public class QuestionService : IQuestionService
    {
        public CrudService<Questions> CrudService { get; set; } = new CrudService<Questions>();
        public CrudService<ObjectiveQuestionOption> ObjectiveOptionCrudService { get; set; } = new CrudService<ObjectiveQuestionOption>();
        public async Task<int> AddQuestionAsync(QuestionViewModel model)
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
                            string questionQuery = "INSERT INTO dbo.Questions VALUES (@QuestionTypeId, @QuestionCategoryId," +
                            "@JobTitleId,@Question,@Attachment,@QuestionComplexityId,@Marks,@PreparedBy,@AuditTs,@Deleted)";
                            var result = await db.ExecuteAsync(questionQuery,
                            new
                            {
                                QuestionTypeId = model.Question.QuestionTypeId,
                                QuestionCategoryId = model.Question.QuestionCategoryId,
                                JobTitleId = model.Question.QuestionTypeId,
                                Question = model.Question.Question,
                                Attachment = model.Question.Attachment,
                                QuestionComplexityId = model.Question.QuestionComplexityId,
                                Marks = model.Question.QuestionComplexityId,
                                PreparedBy = model.Question.PreParedBy,
                                AuditTs = DateTime.Now,
                                Deleted = 0
                            }, tran, null);
                            if (result > 0)
                            {
                                foreach (var item in model.Options)
                                {
                                    item.QuestionId = result;
                                    string optionQuery = "INSERT INTO dbo.ObjectiveQuestionOptions VALUES (@QuestionId, @AnswerOption," +
                                    "@Attachment,@IsAnswer,@Deleted)";
                                    await db.ExecuteAsync(optionQuery,
                                    new
                                    {
                                        QuestionId = item.QuestionId,
                                        AnswerOption = item.AnswerOption,
                                        Attachment = item.Attachment,
                                        IsAnswer = item.IsAnswer,
                                        Deleted = 0
                                    }, tran);
                                }
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
        public async Task<int> DeleteQuestionAsync(long questionId)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    db.Open();
                    string questionQuery = "UPDATE dbo.Questions SET deleted = @delete WHERE QuestionId = @QuestionId";
                    var result = await db.ExecuteAsync(questionQuery,
                            new
                            {
                                delete = 0,
                                QuestionId = questionId
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