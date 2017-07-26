
using VPortal.Core.Data.Crud.Attributes;
using System;
namespace VExam.Api.DTO
{
    [Table("InterviewSessions")]
    public class InterviewSession
    {
        [Key]
        public int InterviewSessionId { get; set; }
        public string Title { get; set; }
        public DateTime SessionStartDate { get; set; }
        public DateTime SessionEndDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime AuditTs { get; set; }
        public bool Deleted { get; set; }
    }
}