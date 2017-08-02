using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VExam.DTO;
using VExam.Services.JobTitle;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{
    [Route("api/v1/jobs")]
    public class JobsApiController : BaseApiController

    {
        private IJobTitleService _jobService;
        private ILogger _logger;

        public JobsApiController(IJobTitleService JobService, ILogger logger)
        {
            _jobService = JobService;
            _logger = logger;
        }

        [HttpPost]
      //  [Authorize]
        [Route("new")]
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
        [Route("get/all")]
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