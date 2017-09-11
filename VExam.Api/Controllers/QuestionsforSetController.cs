using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VExam.DTO;
using VExam.DTO.ViewModel;
using VExam.Services.QuestionsforSets;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{
    [Route("api/v1/examset/question")]
     [AllowAnonymous]
    public class QuestionsforSetController : BaseApiController
    {

        private IQuestionsforSetService _questionService;

        private ILogger _logger;

        public QuestionsforSetController(IQuestionsforSetService questionSetService, ILogger logger)
        {
            _questionService = questionSetService;
            _logger = logger;
        }

        [HttpPost]
        [Route("add")]
        public async Task<ApiResponse> AddQuestonsAsync([FromBody] SetQuestionViewModel model)
        {
            try
            {
                var result = await _questionService.AddQuestionsAsync(model);
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
        public async Task<ApiResponse> UpdateQuestonsAsync([FromBody] SetQuestion model)
        {
            try
            {
                var result = await _questionService.CrudService.UpdateAsync(model);
                return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }

        [HttpPut]
        [Route("delete/{setQuestionId}")]
        public async Task<ApiResponse> DeleteQuestionAsync(long setQuestionId)
        {
            try
            {
                var result = await _questionService.DeleteQuestionAsync(setQuestionId);
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
        public async Task<ApiResponse> GetAllQuestionsBySetId(long examSetId)
        {
            try
            {
                var result = await _questionService.GetQuestionsBySetIdAsync(examSetId);
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