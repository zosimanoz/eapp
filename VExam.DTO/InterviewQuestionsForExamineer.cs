namespace VExam.DTO
{
    public class InterviewQuestionsForExamineer
    {
        public long QuestionId { get; set; }
        public long IntervieweeId { get; set; }
        public string Attachment { get; set; }
        public decimal? Marks { get; set; }
        public string Question { get; set; }
        public long SetQuestionId { get; set; }
        public int QuestionTypeId { get; set; }

        public long AnswerId { get; set; }
        public string subjectiveAnswer { get; set; }
        public long? ResultId { get; set; }
        public decimal MarksObtained { get; set; }
        public string Remarks { get; set; }

    }
}