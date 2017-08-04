

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using VExam.DTO;
using VExam.DTO.ViewModel;
using VPortal.Core.Data;
using VPortal.Core.Data.Crud;

namespace VExam.Services.QuestionsforSets
{
    public class QuestionsforSetService : IQuestionsforSetService
    {
        public CrudService<SetQuestion> CrudService { get; set; } = new CrudService<SetQuestion>();
        

        public async Task<int> DeleteQuestionAsync(long SetQuestionId)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    db.Open();
                    string questionQuery = "UPDATE dbo.SetQuestions SET deleted = @delete WHERE SetQuestionId = @SetQuestionId";
                    var result = await db.ExecuteAsync(questionQuery,
                            new
                            {
                                delete = 1,
                                SetQuestionId = SetQuestionId
                            });
                    return result;

                }
                catch (Exception)
                {
                    throw;
                }

            }

        }
        public async Task<int> AddQuestionsAsync(SetQuestionViewModel model)
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
                            foreach (var item in model.QuestionsForSet)
                            {
                                string optionQuery = "INSERT INTO dbo.SetQuestions VALUES"+
                                " (@ExamSetId, @QuestionId,@CreatedBy)";
                               result= await db.ExecuteAsync(optionQuery,
                                new
                                {
                                    ExamSetId = item.ExamSetId,
                                    QuestionId = item.QuestionId,
                                    CreatedBy = item.CreatedBy
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
        public async Task<IEnumerable<SetQuestion>> GetQuestionsBySetIdAsync(long examSetId)
        {
           try
            {
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {
                   string questionQuery = "SELECT * FROM dbo.QuestionsForSetView "+
                                          "WHERE ExamSetId = @examSetId "+
                                          "ORDER BY QuestionTypeId, QuestionCategoryId,QuestionComplexityId";
                    var result = await db.QueryAsync<SetQuestion>(questionQuery,new{
                        examSetId=examSetId
                    });
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