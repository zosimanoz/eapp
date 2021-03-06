using System.Collections.Generic;

namespace VExam.DTO.ViewModel
{
    public class QuestionViewModel
    {
        public QuestionBanks Question { get; set; }
        public ExamSet ExamSet { get; set; }
        public IEnumerable<ObjectiveQuestionOption> Options { get; set; }
        public AnswersByInterviewees Answers { get; set; }
    }
}