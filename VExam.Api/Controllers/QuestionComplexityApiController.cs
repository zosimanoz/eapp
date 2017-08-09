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
     [Route("api/v1/qustion/complexity")]
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
      //  [Authorize]
        [Route("new")]
        public async Task<ApiResponse> PostAsync([FromBody] QuestionCategory model)
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
      //  [Authorize]
        [Route("get/all")]
        public async Task<ApiResponse> GetAllAsync()
        {
            try
            {
                var result = await _questionComplexityService.CrudService.GetListAsync();
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