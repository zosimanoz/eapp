using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VExam.Api.DTO;
using VExam.Api.Services.JobTitle;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{
    public class JobsController : BaseApiController

    {
        private IJobTitleService _jobService;
        private ILogger _logger;

        public JobsController(IJobTitleService JobService, ILogger logger)
        {
            _jobService = JobService;
            _logger = logger;
        }

        [HttpPost]
      //  [Authorize]
        [Route("api/jobs/new")]
        public async Task<ApiResponse> PostAsync([FromBody] JobTitles model)
        {
            try
            {
                var result = await _jobService.CrudService.InsertAsync(model);
                return HttpResponse(200, "", result.Value);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }
        [HttpGet]
      //  [Authorize]
        [Route("api/jobs/get-all")]
        public async Task<ApiResponse> GetAllAsync()
        {
            try
            {
                var result = await _jobService.CrudService.GetListAsync();
                return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }
    }
}