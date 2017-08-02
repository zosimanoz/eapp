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
        [Route("api/v1/interviewee/new")]
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
      
    }
}