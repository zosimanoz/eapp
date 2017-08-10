using System;
using VPortal.Core.Data.Crud.Attributes;

namespace VExam.DTO
{
    [Table("ExamSets")]
    public class ExamSet
    {
        [Key]
        public long ExamSetId { get; set; }
        public string Title { get; set; }
        public int JobTitleId { get; set; }
        public string Description { get; set; }
        public decimal TotalMark { get; set; }
        public long CreatedBy { get; set; }
        public DateTimeOffset AuditTs { get; set; }
        public bool Deleted { get; set; }
    }
}