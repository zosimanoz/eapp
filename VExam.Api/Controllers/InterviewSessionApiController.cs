using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VExam.DTO;
using VExam.Services.InterviewSessions;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{
    [Route("api/v1/interviewsession")]
    [AllowAnonymous]
    public class InterviewSessionApiController : BaseApiController
    {
        private IInterviewSessionService _interviewSessionService;
        private ILogger _logger;

        public InterviewSessionApiController(IInterviewSessionService interviewSessionService, ILogger logger)
        {
            _interviewSessionService = interviewSessionService;
            _logger = logger;
        }

        [HttpPost]
        [Route("new")]
        public async Task<ApiResponse> PostAsync([FromBody] InterviewSession model)
        {
            try
            {
                Console.WriteLine(model.SessionStartDate);
                Console.WriteLine(model.SessionEndDate);
                //  model.SessionStartDate = DateTime.Parse(model.SessionStartDate.ToString("yyyy-MM-ddTHH:mm:ss.fff"));
                var result = await _interviewSessionService.AddInterviewSessionAsync(model);
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
        public async Task<ApiResponse> UpdateAsync([FromBody] InterviewSession model)
        {
            try
            {
                var result = await _interviewSessionService.UpdateAsync(model);
                return HttpResponse(200, "", result);

            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }
        [HttpGet]
        [Route("active")]
        public async Task<ApiResponse> GetActiveSessionsAsync()
        {
            try
            {
                var result = await _interviewSessionService.GetActiveInterviewSessions();
                return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }
        [HttpGet]
        [Route("get/{id}")]
        public async Task<ApiResponse> GetById(int id)
        {
            try
            {
                var result = await _interviewSessionService.CrudService.GetAsync(id);
                return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }
        [HttpPut]
        [Route("delete/{interviewSessionId}")]
        public async Task<ApiResponse> DeleteAsync(long interviewSessionId)
        {
            try
            {
                var result = await _interviewSessionService.DeleteAsync(interviewSessionId);
                return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }

        [HttpGet]
        [Route("result/summary")]
        public async Task<ApiResponse> ResultAsync(ResultSummary model)
        {
            try
            {
                var result = await _interviewSessionService.ResultSummaryAsync(model);
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