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
using System.Threading;
using System.Collections.Generic;

namespace VExam.Api.Controllers
{

    //[Authorize(Policy = "Admin")]
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
        [Route("update")]
        public async Task<ApiResponse> UpdateAsync([FromBody] QuestionViewModel model)
        {
            try
            {
                var result = await _questionService.UpdateQuestionAsync(model);
                return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }

        }

        [HttpPut]
        [Route("delete/{questionId}")]
        public async Task<ApiResponse> DeleteAsync(long questionId)
        {
            Console.WriteLine("delete api called");
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
        [Route("get/all")]
        public async Task<ApiResponse> GetAllAsync()
        {
            try
            {
                string whereCondition=" where deleted = @delete";
                var result = await _questionService.CrudService.GetListAsync(whereCondition,
                new {
                    delete=0
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
        [Route("get/{questionId}")]
        public async Task<ApiResponse> GetByQuestionIdAsync(long questionId)
        {
            try
            {
                var result = await _questionService.GetQuestionByQuestionIdAsync(questionId);
                return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }
        [HttpGet]
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
        [Route("select")]
        public async Task<ApiResponse> SelectQuestionBankAsync()
        {

            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            foreach (var item in claims)
            {
                Console.WriteLine(item.Type);
            }
            var myUser = HttpContext.User;
            Console.WriteLine(myUser.Identity.IsAuthenticated);
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