using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VExam.Api.DTO;
using VExam.Api.Services.Interviewees;
using VPortal.Core.Log;
using VPortal.Core.Web;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{
    public class RegisterUserApiController : BaseApiController

    {
        private IIntervieweeService _intervieweeService;
        private ILogger _logger;

        public RegisterUserApiController(IIntervieweeService intervieweeService, ILogger logger)
        {
            _intervieweeService = intervieweeService;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<ApiResponse> PostAsync([FromBody] Interviewee model)
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