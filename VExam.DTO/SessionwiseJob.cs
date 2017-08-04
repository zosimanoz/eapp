using System;
using VPortal.Core.Data.Crud.Attributes;


namespace VExam.DTO
{
    [Table("SessionwiseJobs")]
    public class SessionwiseJob
    {
        [Key]
        public long SessionwiseJobId { get; set; }
        public long InterviewSessionId { get; set; }
        public int JobTitleId { get; set; }
        public long ExamSetId { get; set; }
        public long CreatedBy { get; set; }
        public DateTimeOffset AuditTs { get; set; }
        public bool Deleted { get; set; }

    }
}
