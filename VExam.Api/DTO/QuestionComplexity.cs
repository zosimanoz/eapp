
using VPortal.Core.Data.Crud.Attributes;
namespace VExam.Api.DTO
{
    [Table("QuestionComplexities")]
    public class QuestionComplexity
    {
         [Key]
        public int QuestionComplexityId { get; set; }
        public string ComplexityCode { get; set; }
        public string ComplexityTitle { get; set; }
        public bool Deleted { get; set; }
         [IgnoreAll]
        public int RowTotal {get; set;}
    }
}