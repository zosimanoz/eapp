
using System;
using VPortal.Core.Data.Crud.Attributes;

namespace VPortal.Api.DTO
{
    [Table("InterviewSessions")]
    public class InterviewSession
    {
        [Core.Data.Crud.Attributes.Key]
        public int InterviewSessionId { get; set; }
        public string Title { get; set; }
        public DateTime SessionStartDate { get; set; }
        public DateTime SessionEndDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTimeOffset AuditTs { get; set; }
        public bool Deleted { get; set; }
         [IgnoreAll]
        public int RowTotal {get; set;}
    }
}