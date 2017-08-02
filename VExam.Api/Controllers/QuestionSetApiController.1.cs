using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VExam.DTO;
using VExam.DTO.ViewModel;
using VExam.Services.Departments;
using VExam.Services.QuestionSets;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{
    [Route("api/v1/QuestionSet")]
    public class QuestionSetApiController : BaseApiController

    {
        private IQuestionSetService _questionSetService;
        
        private ILogger _logger;

        public QuestionSetApiController(IQuestionSetService questionSetService, ILogger logger)
        {
            _questionSetService = questionSetService;
            _logger = logger;
        }

        [HttpPost]
        //  [Authorize]
        [Route("new/set")]
        public async Task<ApiResponse> PostAsync([FromBody] QuestionSet model)
        {
            try
            {
                var result = await _questionSetService.CrudService.InsertAsync(model);
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
        [Route("get/all/sets")]
        public async Task<ApiResponse> GetAllAsync()
        {
            try
            {
                var result = await _questionSetService.CrudService.GetListAsync();
                return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }

        [HttpPost]
        //  [Authorize]
        [Route("add/questions")]
        public async Task<ApiResponse> addQuestonsAsync([FromBody] SetQuestionViewModel model)
        {
            try
            {
                var result = await _questionSetService.AddQuestionsAsync(model);
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