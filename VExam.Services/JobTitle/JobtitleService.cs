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

namespace VExam.Services.JobTitle
{
    public class JobTitleService : IJobTitleService
    {
        public CrudService<JobTitles> CrudService { get; set; } = new CrudService<JobTitles>();
        public async Task<int> DeleteAsync(int jobTitleId)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    await db.OpenAsync();
                    string deleteQuery = "UPDATE dbo.JobTitles SET deleted = @delete WHERE jobTitleId = @jobTitleId";
                    var result = await db.ExecuteAsync(deleteQuery,
                            new
                            {
                                delete = 1,
                                jobTitleId = jobTitleId
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