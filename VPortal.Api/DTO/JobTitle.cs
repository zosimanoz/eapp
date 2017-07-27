
using VPortal.Core.Data.Crud.Attributes;
namespace VPortal.Api.DTO
{
    [Table("JobTitles")]
    public class JobTitles
    {
        [Key]
        public int JobTitleId { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public bool Deleted { get; set; }
         [IgnoreAll]
        public int RowTotal {get; set;}
    }
}