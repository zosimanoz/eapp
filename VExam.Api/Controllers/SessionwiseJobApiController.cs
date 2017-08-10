using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VExam.DTO;
using VExam.Services.SessionwiseJobs;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{

 [AllowAnonymous]
 [Route("api/v1/sessionwisejob")]
    public class SessionwiseJobApiController : BaseApiController
    {
        private ISessionwiseJobsService _sessionwiseJobsService;
        private ILogger _logger;

        public SessionwiseJobApiController(ISessionwiseJobsService sessionwiseJobsService, ILogger logger)
        {
            _sessionwiseJobsService = sessionwiseJobsService;
            _logger = logger;
        }

        [HttpPost]
        [Route("new")]
        public async Task<ApiResponse> PostAsync([FromBody] SessionwiseJob model)
        {
            try
            {
                model.AuditTs= DateTime.Now;
                if (model.SessionwiseJobId == 0)
                {
                    var result = await _sessionwiseJobsService.CrudService.InsertAsync(model);
                    return HttpResponse(200, "", result.Value);
                }
                else
                {
                    var result = await _sessionwiseJobsService.CrudService.UpdateAsync(model);
                    return HttpResponse(200, "", result);
                }
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }


        [HttpGet]
        [Route("get/{sessionwiseJobId}")]
        public async Task<ApiResponse> GetById(long sessionwiseJobId)
        {
            try
            {
                var result = await _sessionwiseJobsService.CrudService.GetAsync(sessionwiseJobId);
                return HttpResponse(200, "", result);
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
                var result = await _sessionwiseJobsService.CrudService.GetListAsync();
                return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }
         [HttpPut]
        //  [Authorize]
        [Route("delete/{sessionwiseJobId}")]
        public async Task<ApiResponse> DeleteAsync(long sessionwiseJobId)
        {
            try
            {
                var result = await _sessionwiseJobsService.DeleteAsync(sessionwiseJobId);
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
