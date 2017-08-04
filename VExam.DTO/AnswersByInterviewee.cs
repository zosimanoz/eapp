using System;
using VPortal.Core.Data.Crud.Attributes;

namespace VExam.DTO
{
    [Table("AnswersByInterviewees")]
    public class AnswersByInterviewees
    {
        [Key]
        public long AnswerId { get; set; }
        public long IntervieweeId { get; set; }
        public long SetQuestionId { get; set; }
        public string subjectiveAnswer { get; set; }
        public string ObjectiveAnswer { get; set; }
        public long AnsweredBy { get; set; }
        public DateTimeOffset AuditTs { get; set; }
        public bool Deleted { get; set; }
    }
}

