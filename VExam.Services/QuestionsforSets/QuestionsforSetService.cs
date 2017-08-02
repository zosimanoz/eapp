using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VPortal.Core.Data;
using System.Text;
using System;
using VExam.DTO;
using VExam.Services.QuestionSets;
using System.Threading.Tasks;
using VExam.DTO.ViewModel;
using System.Collections.Generic;

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
                                " (@QuestionSetId, @QuestionId,@CreatedBy)";
                               result= await db.ExecuteAsync(optionQuery,
                                new
                                {
                                    QuestionSetId = item.QuestionSetId,
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
        public async Task<IEnumerable<SetQuestion>> GetQuestionsBySetIdAsync(long questionSetId)
        {
           try
            {
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {
                   string questionQuery = "SELECT * FROM dbo.QuestionsForSetView "+
                                          "WHERE QuestionSetId = @questionSetId "+
                                          "ORDER BY QuestionTypeId, QuestionCategoryId,QuestionComplexityId";
                    var result = await db.QueryAsync<SetQuestion>(questionQuery,new{
                        questionSetId=questionSetId
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