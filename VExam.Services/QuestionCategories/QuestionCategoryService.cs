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

namespace VExam.Services.QuestionCategories
{
    public class QuestionCategoryService : IQuestionCategoryService
    {
        public CrudService<QuestionCategory> CrudService { get; set; } = new CrudService<QuestionCategory>();

        public async Task<int> DeleteAsync(int questionCategoryId)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    await db.OpenAsync();
                    string questionQuery = "UPDATE dbo.QuestionCategories SET deleted = @delete WHERE questionCategoryId = @questionCategoryId";
                    var result = await db.ExecuteAsync(questionQuery,
                            new
                            {
                                delete = 1,
                                questionCategoryId = questionCategoryId
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