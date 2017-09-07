using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VPortal.Core.Data;
using System.Text;
using System;
using VExam.DTO;
using VExam.DTO.ViewModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace VExam.Services.Question
{
    public class QuestionService : IQuestionService
    {
        public CrudService<QuestionBanks> CrudService { get; set; } = new CrudService<QuestionBanks>();
        public CrudService<ObjectiveQuestionOption> ObjectiveOptionCrudService { get; set; } = new CrudService<ObjectiveQuestionOption>();
        public async Task<long> AddQuestionAsync(QuestionViewModel model)
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
                            string questionQuery = "INSERT INTO dbo.QuestionBank VALUES (@QuestionTypeId, @QuestionCategoryId," +
                            "@Question,@Attachment,@QuestionComplexityId,@Marks,@PreparedBy,@AuditTs,@Deleted);" +
                            "SELECT CAST(SCOPE_IDENTITY() as bigint)";
                            var result = (await db.QueryAsync<long>(questionQuery,
                            new
                            {
                                QuestionTypeId = model.Question.QuestionTypeId,
                                QuestionCategoryId = model.Question.QuestionCategoryId,
                                Question = model.Question.Question,
                                Attachment = model.Question.Attachment,
                                QuestionComplexityId = model.Question.QuestionComplexityId,
                                Marks = model.Question.Marks,
                                PreparedBy = model.Question.PreParedBy,
                                AuditTs = DateTime.Now,
                                Deleted = 0
                            }, tran, null)).FirstOrDefault();
                            Console.WriteLine(result);
                            if (result > 0)
                            {
                                foreach (var item in model.Options)
                                {
                                    item.QuestionId = result;
                                    Console.WriteLine(item.AnswerOption);
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

        public async Task<long> UpdateQuestionAsync(QuestionViewModel model)
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
                            string questionQuery = "UPDATE dbo.QuestionBank SET " +
                             "QuestionTypeId = @QuestionTypeId, " +
                             "QuestionCategoryId = @QuestionCategoryId," +
                             "Question = @Question," +
                             "Attachment = @Attachment, " +
                             "QuestionComplexityId = @QuestionComplexityId," +
                             "Marks = @Marks," +
                             "AuditTs = @AuditTs " +
                             "WHERE QuestionId = @QuestionId";
                            Console.WriteLine(questionQuery);
                            var result = await db.ExecuteAsync(questionQuery,
                            new
                            {
                                QuestionId = model.Question.QuestionId,
                                QuestionTypeId = model.Question.QuestionTypeId,
                                QuestionCategoryId = model.Question.QuestionCategoryId,
                                Question = model.Question.Question,
                                Attachment = model.Question.Attachment,
                                QuestionComplexityId = model.Question.QuestionComplexityId,
                                Marks = model.Question.Marks,
                                AuditTs = DateTime.Now,
                            }, tran, null);

                            Console.WriteLine(result);
                            if (model.Question.QuestionTypeId == 2)
                            {
                                var response = await this.DeleteOptionByQuestionIdAsync(model.Question.QuestionId);
                                if (response > 0)
                                {
                                    foreach (var item in model.Options)
                                    {
                                        item.QuestionId = model.Question.QuestionId;
                                        Console.WriteLine(item.AnswerOption);
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
                            }

                            tran.Commit();
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

        public async Task<int> DeleteOptionByQuestionIdAsync(long questionId)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    Console.WriteLine("delete called");
                    await db.OpenAsync();
                    string questionQuery = "UPDATE dbo.ObjectiveQuestionOptions SET deleted = @delete WHERE QuestionId = @QuestionId";
                    var result = await db.ExecuteAsync(questionQuery,
                            new
                            {
                                delete = 1,
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
        public async Task<int> DeleteQuestionAsync(long questionId)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    await db.OpenAsync();
                    string questionQuery = "UPDATE dbo.QuestionBank SET deleted = @delete WHERE QuestionId = @QuestionId";
                    var result = await db.ExecuteAsync(questionQuery,
                            new
                            {
                                delete = 1,
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
        public async Task<IEnumerable<QuestionBanks>> SearchQuestionAsync(QuestionSearch model)
        {
            try
            {
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {
                    await db.OpenAsync();
                    string questionQuery = "SELECT * FROM dbo.QuestionBankView WHERE " +
                                             "(@QuestionTypeId IS NULL OR QuestionTypeId = @QuestionTypeId) " +
                                             "AND (@QuestionCategoryId IS NULL OR QuestionCategoryId = @QuestionCategoryId) " +
                                             "AND (@QuestionComplexityId IS NULL OR QuestionComplexityId = @QuestionComplexityId) " +
                                             "AND (Question LIKE @Question)" ;
                    var result = await db.QueryAsync<QuestionBanks>(questionQuery,
                    new
                    {
                        QuestionTypeId = model.QuestionTypeId == 0 ? (int?)null : (int?)model.QuestionTypeId,
                        QuestionCategoryId = model.QuestionCategoryId == 0 ? (int?)null : (int?)model.QuestionCategoryId,
                        QuestionComplexityId = model.QuestionComplexityId == 0 ? (int?)null : (int?)model.QuestionComplexityId,
                        Question = "%" + model.Question + "%"
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
        public async Task<QuestionViewModel> GetQuestionByQuestionIdAsync(long questionId)
        {
            try
            {
                Console.WriteLine(questionId);
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {
                    await db.OpenAsync();
                    string questionQuery = "SELECT * FROM dbo.QuestionBankView " +
                                            "WHERE questionId = @questionId";
                    var question = (await db.QueryAsync<QuestionBanks>(questionQuery, new
                    {
                        questionId = questionId
                    })).FirstOrDefault();

                    var questionModel = new QuestionViewModel();
                    questionModel.Question = question;

                    if (question.QuestionTypeId != 1)
                    {
                        string optionsQuery = "SELECT * FROM dbo.ObjectiveQuestionOptions " +
                                                "WHERE QuestionId = @questionId " +
                                                "AND Deleted = 0";
                        var options = await db.QueryAsync<ObjectiveQuestionOption>(optionsQuery, new
                        {
                            questionId = questionId
                        });
                        questionModel.Options = options;
                    }

                    return questionModel;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<QuestionBanks>> SelectQuestionBankViewAsync()
        {
            try
            {
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {
                    await db.OpenAsync();
                    string questionQuery = "SELECT * FROM dbo.QuestionBankView";
                    var result = await db.QueryAsync<QuestionBanks>(questionQuery);
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