using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using VPortal.Core.Data;
using System.Text;
using System;
using VExam.DTO;
using System.Threading.Tasks;
using VExam.DTO.ViewModel;
using System.Collections.Generic;

namespace VExam.Services.Interviewees
{
    public class IntervieweeService : IIntervieweeService
    {
        public CrudService<Interviewee> CrudService { get; set; } = new CrudService<Interviewee>();

        public async Task<long> DeleteIntervieweeAsync(long intervieweeId)
        {
            var dbfactory = DbFactoryProvider.GetFactory();
            using (var db = (SqlConnection)dbfactory.GetConnection())
            {
                try
                {
                    await db.OpenAsync();
                    string questionQuery = "UPDATE dbo.Interviewees SET deleted = @delete WHERE IntervieweeId = @intervieweeId";
                    var result = await db.ExecuteAsync(questionQuery,
                            new
                            {
                                delete = 1,
                                IntervieweeId = intervieweeId
                            });
                    return result;

                }
                catch (Exception)
                {
                    throw;
                }

            }

        }

        public async Task<bool> IntervieweeValidationAsync(string emailaddress, string contactnumber)
        {
            try
            {
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {
                    await db.OpenAsync();
                    var query = "SELECT * FROM dbo.InterviewSessionCandidate_view WHERE EmailAddress = @EmailAddress" +
                    "AND ContactNumber = @ContactNumber";

                    var result = await db.GetAsync<Interviewee>(query, new
                    {
                        EmailAddress = emailaddress,
                        ContactNumber = contactnumber
                    });
                    if (result != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<QuestionViewModel>> GetinterviewQuestions(long intervieweeId)
        {
            try
            {
                var dbfactory = DbFactoryProvider.GetFactory();
                using (var db = (SqlConnection)dbfactory.GetConnection())
                {
                    await db.OpenAsync();
                    List<QuestionViewModel> question = new List<QuestionViewModel>();
                    var query = "SELECT * FROM dbo.InterviewQuestions WHERE IntervieweeId = @IntervieweeId";
                    var result = await db.QueryAsync<QuestionBanks>(query, new
                    {
                        IntervieweeId = intervieweeId
                    });
                    foreach (var item in result)
                    {
                        // can also check if the question type is subjective or objective;
                        var optionsQuery = "SELECT ObjectiveQuestionOptionId, QuestionId, AnswerOption, Attachment FROM dbo.ObjectiveQuestionOptions WHERE QuestionId = @QuestionId";

                        var options = await db.QueryAsync<ObjectiveQuestionOption>(optionsQuery, new
                        {
                            QuestionId = item.QuestionId
                        });
                        var questionModel = new QuestionViewModel
                        {
                            Question = item,
                            Options = options
                        };
                        question.Add(questionModel);
                    }
                    return question;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}