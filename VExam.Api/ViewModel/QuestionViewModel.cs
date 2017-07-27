using System.Collections.Generic;
using VExam.Api.DTO;

namespace VExam.Api.ViewModel
{
    public class QuestionViewModel
    {
        public Questions Question { get; set; }
        public IEnumerable<ObjectiveQuestionOption> Options { get; set; }
    }
}