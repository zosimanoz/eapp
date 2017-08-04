using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using VExam.DTO;
using VPortal.Core.Data;
using VPortal.Core.Data.Crud;

namespace VExam.Services.ExamSetService
{
    public class ExamSetService : IExamSetService
    {
     public CrudService<ExamSet> CrudService { get; set; } = new CrudService<ExamSet>();

        public async Task<int> DeleteExamSetAsync(long examSetId)
        {
           var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    db.Open();
                    string questionQuery = "UPDATE dbo.ExamSets SET deleted = @delete WHERE ExamSetId = @ExamSetId";
                    var result = await db.ExecuteAsync(questionQuery,
                            new
                            {
                                delete = 1,
                                ExamSetId = examSetId
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