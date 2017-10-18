using System.Collections.Generic;

namespace VExam.DTO.ViewModel
{
    public class InterviewQuestionAnswersheetForExamineerViewModel
    {
        public InterviewQuestionsForExamineer Question { get; set; }
        public IEnumerable<ObjectiveQuestionOption> Options { get; set; }
    }
}