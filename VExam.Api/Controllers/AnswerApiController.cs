using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VExam.DTO.ViewModel;
using VExam.Services.Answers;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{
    [Route("api/v1/answer")]
    public class AnswerApiController:BaseApiController
    {
         private IAnswerService _answerService;
        private ILogger _logger;

        public AnswerApiController(IAnswerService answerService, ILogger logger)
        {
            _answerService = answerService;
            _logger = logger;
        }
        [HttpPost]
        //  [Authorize]
        [Route("save")]
        public async Task<ApiResponse> PostAsync([FromBody] AnswersViewModel model)
        {
            try
            {
               var result = await _answerService.SaveAnswerAsync(model);
               return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }

        //  [HttpGet]
        // //  [Authorize]
        // [Route("filter/{questionType}")]
        // public async Task<ApiResponse> FilterAsync(int questionType)
        // {
        //     try
        //     {
        //        var result = await _answerService.FilterAnswerAsync(questionType);
        //        return HttpResponse(200, "", result);
        //     }
        //     catch (Exception e)
        //     {
        //         _logger.Log(LogType.Error, () => e.Message, e);
        //         return HttpResponse(500, e.Message);
        //     }
        // }
    }
}