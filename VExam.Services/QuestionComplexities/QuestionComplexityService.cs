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

namespace VExam.Services.QuestionComplexities
{
    public class QuestionComplexityService : IQuestionComplexityService
    {
        public CrudService<QuestionComplexity> CrudService { get; set; } = new CrudService<QuestionComplexity>();
        public async Task<int> DeleteAsync(int questionComplexityId)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    await db.OpenAsync();
                    string questionQuery = "UPDATE dbo.QuestionComplexities SET deleted = @delete WHERE QuestionComplexityId = @questionComplexityId";
                    var result = await db.ExecuteAsync(questionQuery,
                            new
                            {
                                delete = 1,
                                questionComplexityId = questionComplexityId
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