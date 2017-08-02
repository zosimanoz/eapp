using VPortal.Core.Data.Crud.Attributes;

namespace VExam.DTO
{
    public class QuestionSearch
    {
        public int QuestionTypeId { get; set; }
        public int QuestionCategoryId { get; set; }
        public int JobTitleId { get; set; }
        public int QuestionComplexityId { get; set; }
        public string Question { get; set; }

        [IgnoreAll]
        public int RowTotal { get; set; }
    }
}