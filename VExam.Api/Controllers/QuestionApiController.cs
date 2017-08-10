using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using VExam.DTO;
using VExam.Services.Question;
using VExam.DTO.ViewModel;
using VPortal.Core.Data;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;

namespace VExam.Api.Controllers
{

    [Route("api/v1/questionbank")]
   //[Authorize]
    [AllowAnonymous]
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

        [HttpPut]
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

        [HttpPut]
        //  [Authorize]
        [Route("delete/{questionId}")]
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
        [Route("get/all")]
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
        [HttpGet]
        //  [Authorize]
        [Route("search")]
        public async Task<ApiResponse> SearchQuestionBankAsync(QuestionSearch model)
        {

            try
            {
                var result = await _questionService.SearchQuestionAsync(model);
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
        [Route("select")]
        public async Task<ApiResponse> SelectQuestionBankAsync()
        {
            try
            {
                var result = await _questionService.SelectQuestionBankViewAsync();
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