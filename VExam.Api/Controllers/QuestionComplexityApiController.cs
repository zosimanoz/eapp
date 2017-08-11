using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VExam.DTO;
using VExam.Services.QuestionCategories;
using VExam.Services.QuestionComplexities;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{
    [Route("api/v1/question/complexity")]
    [AllowAnonymous]
    public class QuestionComplexityApiController : BaseApiController
    {

        private IQuestionComplexityService _questionComplexityService;
        private ILogger _logger;

        public QuestionComplexityApiController(IQuestionComplexityService questoinComplexity, ILogger logger)
        {
            _questionComplexityService = questoinComplexity;
            _logger = logger;
        }

        [HttpPost]
        [Route("new")]
        public async Task<ApiResponse> PostAsync([FromBody] QuestionComplexity model)
        {
            try
            {
                var result = await _questionComplexityService.CrudService.InsertAsync(model);
                return HttpResponse(200, "", result.Value);
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
                var result = await _questionComplexityService.CrudService.GetListAsync(whereCondition,
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

        [HttpPut]
        [Route("update")]
        public async Task<ApiResponse> UpdateAsync([FromBody] QuestionComplexity model)
        {
            try
            {
                var result = await _questionComplexityService.CrudService.UpdateAsync(model);
                return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }
        [HttpGet]
        [Route("get/{questionComplexityId}")]
        public async Task<ApiResponse> GetById(int questionComplexityId)
        {
            try
            {
                var result = await _questionComplexityService.CrudService.GetAsync(questionComplexityId);
                return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }
        [HttpPut]
        [Route("delete/{questionComplexityId}")]
        public async Task<ApiResponse> DeleteAsync(int questionComplexityId)
        {
            try
            {
                var result = await _questionComplexityService.DeleteAsync(questionComplexityId);
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