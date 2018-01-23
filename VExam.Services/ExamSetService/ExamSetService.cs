using System;
using System.Collections.Generic;
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
                    await db.OpenAsync();
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

        public async Task<IEnumerable<ExamSet>> GetExamSetsByJobTitleAsync(long jobTitleId)
        {
            try
            {
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {
                    await db.OpenAsync();
                    string examSetsQuery = "SELECT * FROM ExamSets where JobTitleId = @jobTitleId AND Deleted = @deleted";
                    var result = await db.QueryAsync<ExamSet>(examSetsQuery,
                    new
                    {
                        jobTitleId =jobTitleId,
                        deleted=0
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