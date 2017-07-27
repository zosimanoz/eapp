using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using VExam.Api.DTO;
using VExam.Api.Services.Question;
using VExam.Api.ViewModel;
using VPortal.Core.Data;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{
    [Route("api/question")]
    public class QuestionApiController : BaseApiController
    {

        private IQuestionService _questionService;
        private ILogger _logger;

        public QuestionApiController(IQuestionService questionService, ILogger logger)
        {
            _questionService = questionService;
            _logger = logger;
        }


        [HttpPost]
        //  [Authorize]
        [Route("new")]
        public async Task<ApiResponse> PostAsync([FromBody] QuestionViewModel model)
        {
            try
            {
                var result = await _questionService.AddQuestionAsync(model);
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
        [Route("update")]
        public async Task<ApiResponse> UpdateAsync([FromBody] QuestionViewModel model)
        {
            try
            {
                var result = await _questionService.CrudService.UpdateAsync(model.Question);
                if (model.Question.QuestionTypeId == 2) // 1= subjective , 2 = objective
                {
                    foreach (var item in model.Options)
                    {
                        await _questionService.ObjectiveOptionCrudService.UpdateAsync(item);
                    }
                }
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
        [Route("delete")]
        public async Task<ApiResponse> DeleteAsync(long questionId)
        {
            try
            {
                var result = await _questionService.DeleteQuestionAsync(questionId);
                return HttpResponse(200, "", result);
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
                var result = await _questionService.CrudService.GetListAsync();
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