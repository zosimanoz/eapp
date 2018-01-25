using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VExam.DTO;
using VExam.Services.ExamSetService;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{
    [Route("api/v1/examset")]
    [AllowAnonymous]
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
        [Route("delete/{examSetId}")]
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
        [HttpPut]
        [Route("update")]
        public async Task<ApiResponse> UpdateAsync([FromBody] DTO.ExamSet model)
        {
            Console.WriteLine(model.Title);
           
            try
            {
                var result = await _examSetService.CrudService.UpdateAsync(model);
                return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }

        [HttpGet]
        [Route("get/all")]
        public async Task<ApiResponse> GetAllAsync()
        {
            try
            {
                string whereCondition = " where deleted = @delete";
                var result = await _examSetService.CrudService.GetListAsync(whereCondition,
                new
                {
                    delete = 0
                });
                return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }

        [HttpGet]
        [Route("get/{examSetId}")]
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
         [HttpGet]
        [Route("by-jobtitle/{jobTitleId}")]
        public async Task<ApiResponse> GetExamSetByJobTitleAsync(long jobTitleId)
        {
            try
            {
                var result = await _examSetService.GetExamSetsByJobTitleAsync(jobTitleId);
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