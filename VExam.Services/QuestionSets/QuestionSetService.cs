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

namespace VExam.Services.QuestionSets
{
    public class QuestionSetService : IQuestionSetService
    {
        public CrudService<QuestionSet> CrudService { get; set; } = new CrudService<QuestionSet>();
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

    }
}