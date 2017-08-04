using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VExam.DTO;
using VExam.Services.ExamSetService;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{
    [Route("api/v1/examset")]
    public class ExamSetsApiController : BaseApiController
    {
        private IExamSetService _examSetService;

        private ILogger _logger;
         public ExamSetsApiController(IExamSetService examSetService, ILogger logger)
        {
            _examSetService = examSetService;
            _logger = logger;
        }
        [HttpPost]
        [Route("new/set")]
        public async Task<ApiResponse> PostAsync([FromBody] DTO.ExamSet model)
        {
            try
            {
                var result = await _examSetService.CrudService.InsertAsync(model);
                return HttpResponse(200, "", result.Value);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }
         [HttpPut]
        [Route("delete/set/{examSetId}")]
        public async Task<ApiResponse> DeleteAsync(long examSetId)
        {
            try
            {
                var result = await _examSetService.DeleteExamSetAsync(examSetId);
                return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }
        [HttpGet]
        [Route("get/all/sets")]
        public async Task<ApiResponse> GetAllAsync()
        {
            try
            {
                var result = await _examSetService.CrudService.GetListAsync();
                return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }

        [HttpGet]
        [Route("get/set/{examSetId}")]
        public async Task<ApiResponse> GetAsync(long examSetId)
        {
            try
            {
                var result = await _examSetService.CrudService.GetAsync(examSetId);
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