
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
        public string Question { get; set; }
        public string Attachment { get; set; }
        public int QuestionComplexityId { get; set; }
        public decimal? Marks { get; set; }
        public long PreParedBy { get; set; }
        public DateTimeOffset? AuditTs { get; set; }
        public bool Deleted { get; set; }
        
        [IgnoreAll]
        public int AnsCount {get; set;}
        [IgnoreAll]
        public string QuestionTypeName { get; set; }
        [IgnoreAll]
        public string QuestionCategoryName { get; set; }
        [IgnoreAll]
        public string QuestionTypeCode { get; set; }
        [IgnoreAll]
        public string QuestionCategoryCode { get; set; }
        [IgnoreAll]
        public string QuestionComplexityName { get; set; }
        [IgnoreAll]
        public string QuestionComplexityCode { get; set; }
        [IgnoreAll]
        public string JobTitle { get; set; }
        [IgnoreAll]
        public long IntervieweeId { get; set; }
        [IgnoreAll]

        public long SetQuestionId { get; set; }
        [IgnoreAll]
        public int SN { get; set; }
        [IgnoreAll]
        public int RowTotal { get; set; }
    }
}