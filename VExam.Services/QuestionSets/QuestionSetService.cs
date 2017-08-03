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
        

        public async Task<int> DeleteQuestionSetAsync(long questionSetId)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    db.Open();
                    string questionQuery = "UPDATE dbo.QuestionSets SET deleted = @delete WHERE QuestionSetId = @QuestionSetId";
                    var result = await db.ExecuteAsync(questionQuery,
                            new
                            {
                                delete = 1,
                                QuestionSetId = questionSetId
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