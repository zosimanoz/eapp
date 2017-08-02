
using VPortal.Core.Data.Crud.Attributes;
using System;
namespace VExam.DTO
{
    [Table("QuestionBank")]
    public class QuestionBanks
    {
        [Key]
        public long QuestionId { get; set; }
        public int QuestionTypeId { get; set; }
        public int QuestionCategoryId { get; set; }
        public int JobTitleId { get; set; }
        public string Question { get; set; }
        public string Attachment { get; set; }
        public int QuestionComplexityId { get; set; }
        public decimal?  Marks { get; set; }
        public long PreParedBy { get; set; }
        public DateTimeOffset? AuditTs { get; set; }
        public bool Deleted { get; set; }
         [IgnoreAll]
        public int RowTotal {get; set;}
    }
}