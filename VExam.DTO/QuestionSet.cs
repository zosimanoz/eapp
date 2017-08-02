using System;
using VPortal.Core.Data.Crud.Attributes;

namespace VExam.DTO
{
    [Table("QuestionSets")]
    public class QuestionSet
    {
        [Key]
        public long QuestionSetId { get; set; }
        public string Title { get; set; }
        public int JobTitleId { get; set; }
        public long CreatedBy { get; set; }
        public DateTimeOffset AuditTs { get; set; }
        public bool Deleted { get; set; }
    }
}