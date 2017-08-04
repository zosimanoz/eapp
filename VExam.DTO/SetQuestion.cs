using System;
using VPortal.Core.Data.Crud.Attributes;

namespace VExam.DTO
{
    [Table("SetQuestions")]
    public class SetQuestion
    {
        [Key]
        public long SetQuestionId { get; set; }
        public long ExamSetId { get; set; }
        public long QuestionId { get; set; }
        public long CreatedBy { get; set; }
        public DateTimeOffset AuditTs { get; set; }
        public bool Deleted { get; set; }
        [IgnoreAll]
        public int QuestionTypeId { get; set; }
        [IgnoreAll]
        public string QuestionTypeName { get; set; }
        [IgnoreAll]
        public string QuestionTypeCode { get; set; }
        [IgnoreAll]
        public int QuestionCategoryId { get; set; }
        [IgnoreAll]
        public string QuestionCategoryCode { get; set; }
        [IgnoreAll]
        public string QuestionCategoryName { get; set; }
        [IgnoreAll]
        public int JobTitleId { get; set; }
        [IgnoreAll]
        public string JobTitle { get; set; }
        [IgnoreAll]
        public string Question { get; set; }
        [IgnoreAll]
        public string Attachment { get; set; }
        [IgnoreAll]
        public int QuestionComplexityId { get; set; }
        [IgnoreAll]
        public string QuestionComplexityName { get; set; }
        [IgnoreAll]
        public string QuestionComplexityCode { get; set; }
      

    }
}
