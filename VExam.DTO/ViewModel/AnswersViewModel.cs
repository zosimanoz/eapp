using System.Collections.Generic;

namespace VExam.DTO.ViewModel
{
    public class AnswersViewModel
    {
        public IEnumerable<AnswersByInterviewees> Answer { get; set; }
    }
}