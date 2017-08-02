using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{
    public class SignoutApiController : BaseApiController
    {

        private ILogger _logger;

        public SignoutApiController(ILogger logger)
        {
            _logger = logger;
        }
        [HttpPost]
        [Route("api/v1/logout")]
        public async Task<ApiResponse> Logout()
        {
            try
            {
                Console.WriteLine("Logging out...");
                await HttpContext.Authentication.SignOutAsync("Cookie");
                return HttpResponse(500, "true");
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }

        }
    }
}