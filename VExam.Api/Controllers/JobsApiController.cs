using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VExam.DTO;
using VExam.Services.JobTitle;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{
    [Route("api/v1/jobs")]
    [AllowAnonymous]
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
                string whereCondition = " where deleted = @delete";
                var result = await _jobService.CrudService.GetListAsync(whereCondition,
                new
                {
                    delete = 0
                });
                return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }


        [HttpPut]
        [Route("update")]
        public async Task<ApiResponse> UpdateAsync([FromBody] JobTitles model)
        {
            try
            {
                var result = await _jobService.CrudService.UpdateAsync(model);
                return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }
        [HttpGet]
        [Route("get/{jobTitleId}")]
        public async Task<ApiResponse> GetById(int jobTitleId)
        {
            try
            {
                var result = await _jobService.CrudService.GetAsync(jobTitleId);
                return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }
        [HttpPut]
        [Route("delete/{jobTitleId}")]
        public async Task<ApiResponse> DeleteAsync(int jobTitleId)
        {
            try
            {
                var result = await _jobService.DeleteAsync(jobTitleId);
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