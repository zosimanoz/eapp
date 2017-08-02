using System;
using VPortal.Core.Data.Crud.Attributes;

namespace VExam.DTO
{
     [Table("SetQuestions")]
    public class SetQuestion
    {
        [Key]
        public long SetQuestion_id { get; set; }
        public long QuestionSetId { get; set; }
        public long QuestionId { get; set; }
        public long CreatedBy { get; set; }
        public DateTimeOffset AuditTs { get; set; }
        public bool Deleted { get; set; }

    }
}