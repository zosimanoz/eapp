
using VPortal.Core.Data.Crud.Attributes;
namespace VPortal.Api.DTO
{
    [Table("QuestionCategories")]
    public class QuestionCategory
    {
         [Core.Data.Crud.Attributes.Key]
        public int QuestionCategoryId { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public bool Deleted { get; set; }
         [IgnoreAll]
        public int RowTotal {get; set;}
    }
}