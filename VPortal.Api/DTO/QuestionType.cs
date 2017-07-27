
using VPortal.Core.Data.Crud.Attributes;
namespace VPortal.Api.DTO
{
    [Table("QuestionType")]
    public class QuestionType
    {
        [Core.Data.Crud.Attributes.Key]
        public int QuestionTypeId { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public bool Deleted { get; set; }
         [IgnoreAll]
        public int RowTotal {get; set;}
    }
}