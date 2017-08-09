using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VExam.DTO;
using VExam.Services.Departments;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;


namespace VExam.Api.Controllers
{
    [Route("api/v1/department")]
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
        [Route("new")]
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
                return HttpResponse(500, model.DepartmentCode);
            }
        }


           [HttpPost]
        //  [Authorize]
        [Route("update")]
        public async Task<ApiResponse> UpdateAsync([FromBody] Department model)
        {
            try
            {
                var result = await _departmentService.CrudService.UpdateAsync(model);
                return HttpResponse(200, "", result);
             
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, model.DepartmentCode);
            }
        }


        [HttpGet]
        [Route("get/{id}")]
        public async Task<ApiResponse> GetById(int id){
            try{
                var result = await _departmentService.CrudService.GetAsync(id);
                return HttpResponse(200, "", result);
            }catch(Exception e){
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