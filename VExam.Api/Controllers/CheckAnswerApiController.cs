using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VExam.DTO;
using VExam.Services.Answers;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{
    [Route("api/v1/checkanswer")]
      [AllowAnonymous]
    public class CheckAnswerApiController:BaseApiController
    {
        private IAnswerService _answerService;
        private ILogger _logger;

        public CheckAnswerApiController(IAnswerService answerService, ILogger logger)
        {
            _answerService = answerService;
            _logger = logger;
        }
        [HttpGet]
        //  [Authorize]
        [Route("answersheet/{intervieweeId}")]
        public async Task<ApiResponse> GetAsync(long intervieweeId)
        {
            try
            {
               var result = await _answerService.GetInterviewQuestionAnswerSheet(intervieweeId);
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
        [Route("all/answersheet/examineer/{intervieweeId}")]
        public async Task<ApiResponse> GetInterviewAnswerSheetForExaminer(long intervieweeId)
        {
            try
            {
               var result = await _answerService.GetInterviewAnswerSheetForExamineer(intervieweeId);
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
        [Route("subjective/answersheet/examineer/{intervieweeId}")]
        public async Task<ApiResponse> GetInterviewSubjectiveAnswerSheetForExaminer(long intervieweeId)
        {
            try
            {
               var result = await _answerService.GetInterviewSubjectiveAnswerSheetForExamineer(intervieweeId);
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
        [Route("objective/answersheet/examineer/{intervieweeId}")]
        public async Task<ApiResponse>GetInterviewObjectiveAnswerSheetForExaminer (long intervieweeId)
        {
            try
            {
               var result = await _answerService.GetInterviewObjectiveAnswerSheetForExamineer(intervieweeId);
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
        [Route("check/objectiveanswer/{intervieweeId}")]
        public async Task<ApiResponse>  checkObjectiveAnswers(long intervieweeId)
        {
             try
            {
              await _answerService.CheckObjectiveAnswers(intervieweeId);
               return HttpResponse(200, "");
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }
        [HttpPost]
        //  [Authorize]
        [Route("check/subjectiveanswer")]
        public async Task<ApiResponse> checkSubjectiveAnswers([FromBody] Result model)
        {
             try
            {
              await _answerService.CheckAnswer(model);
               return HttpResponse(200, "");
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }
    }
}