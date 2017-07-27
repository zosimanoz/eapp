using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VExam.Api.DTO;
using VExam.Api.Services.Departments;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{
    public class DepartmentApiController : BaseApiController

    {
        private IDepartmentService _departmentService;
        private ILogger _logger;

        public DepartmentApiController(IDepartmentService departmentService, ILogger logger)
        {
            _departmentService = departmentService;
            _logger = logger;
        }

        [HttpPost]
      //  [Authorize]
        [Route("api/department/new")]
        public async Task<ApiResponse> PostAsync([FromBody] Department model)
        {
            try
            {
                var result = await _departmentService.CrudService.InsertAsync(model);
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
        [Route("api/department/get-all")]
        public async Task<ApiResponse> GetAllAsync()
        {
            try
            {
                var result = await _departmentService.CrudService.GetListAsync();
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