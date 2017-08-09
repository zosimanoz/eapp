using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VExam.DTO;
using VExam.Services.QuestionCategories;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{
    [Route("api/v1/question/category")]
    [AllowAnonymous]
    public class QuestionCategoryApiController : BaseApiController
    {

        private IQuestionCategoryService _questionCategoryService;
        private ILogger _logger;

        public QuestionCategoryApiController(IQuestionCategoryService questoinCategory, ILogger logger)
        {
            _questionCategoryService = questoinCategory;
            _logger = logger;
        }

        [HttpPost]
        [Route("new")]
        public async Task<ApiResponse> PostAsync([FromBody] QuestionCategory model)
        {
            try
            {
                var result = await _questionCategoryService.CrudService.InsertAsync(model);
                return HttpResponse(200, "", result.Value);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }
        [HttpPut]
        [Route("update")]
        public async Task<ApiResponse> UpdateAsync([FromBody] QuestionCategory model)
        {
            try
            {
                var result = await _questionCategoryService.CrudService.UpdateAsync(model);
                return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }
        [HttpGet]
        [Route("get/{questionCategoryId}")]
        public async Task<ApiResponse> GetById(int questionCategoryId)
        {
            try
            {
                var result = await _questionCategoryService.CrudService.GetAsync(questionCategoryId);
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
                var result = await _questionCategoryService.CrudService.GetListAsync(whereCondition,
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
        [Route("delete/{questionCategoryId}")]
        public async Task<ApiResponse> DeleteAsync(int questionCategoryId)
        {
            try
            {
                var result = await _questionCategoryService.DeleteAsync(questionCategoryId);
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