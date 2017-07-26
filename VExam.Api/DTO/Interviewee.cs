
using VPortal.Core.Data.Crud.Attributes;
namespace VExam.Api.DTO
{
    [Table("Interviewees")]
    public class Interviewee
    {
        [Key]
        public long IntervieweeId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string ProfilePicture { get; set; }
        public bool Deleted { get; set; }
    }
}