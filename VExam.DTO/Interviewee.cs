
using VPortal.Core.Data.Crud.Attributes;
namespace VExam.DTO
{
    [Table("Interviewees")]
    public class Interviewee
    {
        [Key]
        public long IntervieweeId { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public long InterviewSessionId { get; set; }
        public int JobTitleId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        public string Attachments { get; set; }
        public bool AttendedExam { get; set; }
        public bool Deleted { get; set; }
        [IgnoreAll]
        public string SessionTitle { get; set; }
        [IgnoreAll]
        public string JobTitle { get; set; }
        [IgnoreAll]
        public string FullName { get; set; }
        [IgnoreAll]
        public int RowTotal { get; set; }
        [IgnoreAll]
        public decimal TotalMarksObtained { get; set; }
    }
}