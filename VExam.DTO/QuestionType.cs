
using VPortal.Core.Data.Crud.Attributes;
namespace VExam.DTO
{
    [Table("QuestionTypes")]
    public class QuestionType
    {
        [Key]
        public int QuestionTypeId { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public bool Deleted { get; set; }
         [IgnoreAll]
        public int RowTotal {get; set;}
    }
}