
using VPortal.Core.Data.Crud.Attributes;
using System;
namespace VExam.DTO
{
    [Table("InterviewSessions")]
    public class InterviewSession
    {
        [Key]
        public int InterviewSessionId { get; set; }
        public string Title { get; set; }
        public DateTime SessionStartDate { get; set; }
        public DateTime SessionEndDate { get; set; }
        public int JobTitleId { get; set; }
        [IgnoreAll]
        public string JobTitle { get; set; }
        public long CreatedBy { get; set; }
        public DateTime AuditTs { get; set; }
        public bool Deleted { get; set; }
        [IgnoreAll]
        public int RowTotal { get; set; }
    }
}