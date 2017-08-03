using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VExam.DTO;
using VExam.Services.Interviewees;
using VPortal.Core.Log;
using VPortal.Core.Web;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{
    [Route("api/v1/interviewee")]
    public class IntervieweeApiController : BaseApiController

    {
        private IIntervieweeService _intervieweeService;
        private ILogger _logger;

        public IntervieweeApiController(IIntervieweeService intervieweeService, ILogger logger)
        {
            _intervieweeService = intervieweeService;
            _logger = logger;
        }

        [HttpPost]
        [Route("new")]
        public async Task<ApiResponse> CreateAsync([FromBody] Interviewee model)
        {
            try
            {
                var result = await _intervieweeService.CrudService.InsertAsync(model);
                return HttpResponse(200, "", result.Value);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }

        [HttpPut]
        [Route("delete")]
        public async Task<ApiResponse> DeleteAsync(long intervieweeId)
        {
            try
            {
                var result = await _intervieweeService.DeleteIntervieweeAsync(intervieweeId);
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
        public async Task<ApiResponse> UpdateAsync([FromBody] Interviewee model)
        {
            try
            {
                var result = await _intervieweeService.CrudService.UpdateAsync(model);
                return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }
        [HttpGet]
        [Route("interview/questions/{intervieweeId}")]
        public async Task<ApiResponse> GetQuestionAsync(long intervieweeId)
        {
            try
            {
                var result = await _intervieweeService.GetinterviewQuestions(intervieweeId);
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