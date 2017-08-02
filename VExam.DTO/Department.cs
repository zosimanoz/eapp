
using VPortal.Core.Data.Crud.Attributes;
namespace VExam.DTO
{
    [Table("Departments")]
    public class Department 
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public bool Deleted { get; set; }
         [IgnoreAll]
        public int RowTotal {get; set;}
    }
}