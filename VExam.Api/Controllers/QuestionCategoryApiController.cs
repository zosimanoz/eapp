using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VExam.DTO;
using VExam.Services.QuestionCategories;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{
     [Route("api/v1/qustion/category")]
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
      //  [Authorize]
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
        [HttpGet]
      //  [Authorize]
        [Route("get/all")]
        public async Task<ApiResponse> GetAllAsync()
        {
            try
            {
                var result = await _questionCategoryService.CrudService.GetListAsync();
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