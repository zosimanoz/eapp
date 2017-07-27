using VPortal.Core.Data.Crud.Attributes;

namespace VExam.Api.DTO
{
    public class User
    {
      public long UserId { get; set; }  
      public int RoleId { get; set; }
      public string EmailAddress { get; set; }
      public string INumber { get; set; }
      public string FirstName { get; set; }
      public string MiddleName { get; set; }
      public string LastName { get; set; }
      public string Address { get; set; }
      public string ContactNumber { get; set; }
      public int DepartmentId { get; set; }
      public string ProfilePicture { get; set; }
      public bool Deleted { get; set; }
       [IgnoreAll]
        public int RowTotal {get; set;}

    }
}
