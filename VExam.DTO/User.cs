using VPortal.Core.Data.Crud.Attributes;

namespace VExam.DTO
{
    [Table("Users")]
    public class User
    {
        [Key]
        public long UserId { get; set; }
        public string RoleId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public bool Deleted { get; set; }
        public bool PasswordChanged { get; set; }

        [IgnoreAll]
        public int RowTotal { get; set; }
        [IgnoreAll]
        public string DepartmentName { get; set; }
        [IgnoreAll]
        public string RoleTitle { get; set; }
    }
}
