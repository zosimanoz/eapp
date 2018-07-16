using VPortal.Core.Data.Crud.Attributes;

namespace VExam.DTO
{
    [Table("ObjectiveQuestionOptions")]
    public class ObjectiveQuestionOption
    {
        [Key]
        public long ObjectiveQuestionOptionId { get; set; }
        public long QuestionId { get; set; }
        public string AnswerOption { get; set; }
        public string Attachment { get; set; }
        public bool IsAnswer { get; set; }
        public bool Deleted { get; set; }
      
        [IgnoreAll]
        public bool AnswerByInterviewees { get; set; }
    }
}

