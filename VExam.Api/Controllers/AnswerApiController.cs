using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VExam.DTO;
using VExam.DTO.ViewModel;
using VExam.Services.Answers;
using VExam.Services.Interviewees;
using VExam.Services.Users;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{
    [Route("api/v1/answer")]
    
    public class AnswerApiController:BaseApiController
    {
         private IAnswerService _answerService;
         private IIntervieweeService _intervieweeService;
        private ILogger _logger;

        public AnswerApiController(IAnswerService answerService,IIntervieweeService intervieweeService, ILogger logger)
        {
            _answerService = answerService;
            _logger = logger;
            _intervieweeService = intervieweeService;
        }

        [HttpPost]
        [Route("save")]
      //  [Authorize]
        public async Task<ApiResponse> PostAsync([FromBody] IEnumerable<QuestionViewModel> model)
        {
            // var intervieweeId = model.First().Answers.IntervieweeId;

            // var intervieweeDetail = await _intervieweeService.GetIntervieweeDetailById(intervieweeId);
            // Console.WriteLine(intervieweeDetail.AttendedExam);
            // if(intervieweeDetail.AttendedExam) {
            //     return HttpResponse(500, "Paper has been submitted.");
            // }
            List<AnswersByInterviewees> answerList = new List<AnswersByInterviewees>();
            foreach (var item in model)
            {
                //Console.WriteLine(item.Question.QuestionTypeId);
                if(item.Answers !=null){
                    string objectiveAnswers="";
                    if(item.Question.QuestionTypeId == 2){
                        foreach(var option in item.Options){
                            if(option.AnswerByInterviewees){
                                objectiveAnswers = objectiveAnswers+","+option.ObjectiveQuestionOptionId.ToString();
                            }
                        }
                    item.Answers.ObjectiveAnswer = objectiveAnswers.Trim(',');
                    }
                    answerList.Add(item.Answers);
                }
            }
            try
            {
               var result = await _answerService.SaveAnswerAsync(answerList);
               return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
           // return HttpResponse(500, "", model);
        }


        // public async Task<ApiResponse> PostAsync([FromBody] AnswersViewModel model)
        // {
        //     try
        //     {
        //        var result = await _answerService.SaveAnswerAsync(model);
        //        return HttpResponse(200, "", result);
        //     }
        //     catch (Exception e)
        //     {
        //         _logger.Log(LogType.Error, () => e.Message, e);
        //         return HttpResponse(500, e.Message);
        //     }
        // }

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