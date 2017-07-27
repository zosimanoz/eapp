namespace VExam.Api.DTO
{
    public class ObjectiveQuestionOption
    {
        public long ObjectiveQuestionOptionId { get; set; }
        public long QuestionId { get; set; }
        public string AnswerOption { get; set; }
        public string Attachment { get; set; }
        public bool IsAnswer { get; set; }
        public bool Deleted { get; set; }
        
    }
}

