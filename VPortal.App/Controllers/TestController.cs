using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VPortal.App.Services;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VPortal.App.Controllers
{
    [Route("api/v1/[controller]")]
    public class TestController : BaseApiController
    {

        private ITestTableService _testService;
        private ILogger _logger;

        public TestController(ITestTableService testService, ILogger logger)
        {
            _testService = testService;
            _logger = logger;
        }


        [HttpGet]
        [Route("all")]
        public async Task<ApiResponse> GetAllData()
        {
            try
            {
                var data = await _testService.CrudService.GetListAsync();
                return HttpResponse(200, "", data);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }

        }

    }
}
