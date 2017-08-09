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
                    db.Open();
                    using (IDbTransaction tran = db.BeginTransaction())
                    {
                        try
                        {
                            string questionQuery = "INSERT INTO dbo.QuestionBank VALUES (@QuestionTypeId, @QuestionCategoryId," +
                            "@JobTitleId,@Question,@Attachment,@QuestionComplexityId,@Marks,@PreparedBy,@AuditTs,@Deleted);"+
                            "SELECT CAST(SCOPE_IDENTITY() as bigint)";
                            var result = (await db.QueryAsync<long>(questionQuery,
                            new
                            {
                                QuestionTypeId = model.Question.QuestionTypeId,
                                QuestionCategoryId = model.Question.QuestionCategoryId,
                                JobTitleId = model.Question.JobTitleId,
                                Question = model.Question.Question,
                                Attachment = model.Question.Attachment,
                                QuestionComplexityId = model.Question.QuestionComplexityId,
                                Marks = model.Question.QuestionComplexityId,
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
                   string questionQuery = "SELECT * FROM dbo.QuestionBankView WHERE " +
                                            "(@QuestionTypeId IS NULL OR QuestionTypeId = @QuestionTypeId) " +
                                            "AND (@QuestionCategoryId IS NULL OR QuestionCategoryId = @QuestionCategoryId) "+
                                            "AND (@JobTitleId IS NULL OR JobTitleId = @JobTitleId) "+
                                            "AND (@QuestionComplexityId IS NULL OR QuestionComplexityId = @QuestionComplexityId) "+
                                             "AND (Question LIKE @Question)";
                    var result = await db.QueryAsync<QuestionBanks>(questionQuery,
                    new
                    {
                     QuestionTypeId = model.QuestionTypeId ==0?(int?)null: (int?)model.QuestionTypeId,
                     QuestionCategoryId = model.QuestionCategoryId ==0?(int?)null: (int?)model.QuestionCategoryId,  
                     JobTitleId = model.JobTitleId ==0?(int?)null: (int?)model.JobTitleId  ,
                     QuestionComplexityId = model.QuestionComplexityId ==0?(int?)null: (int?)model.QuestionComplexityId  ,
                     Question = "%"+model.Question+"%"   
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

         public async Task<IEnumerable<QuestionBanks>> SelectQuestionBankViewAsync()
        {
            try
            {
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {
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