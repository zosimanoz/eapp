using System;
using VPortal.Core.Data.Crud.Attributes;

namespace VExam.DTO
{
    [Table("Results")]
    public class Result
    {
        [Key]
        public long? ResultId { get; set; }
        public long AnswerId { get; set; }
        public decimal MarksObtained { get; set; }
        public string Remarks { get; set; }
        public int CheckedBy { get; set; }
        public long ExaminerId { get; set; }
        public DateTimeOffset AuditTs { get; set; }
        public bool Deleted { get; set; }
    }
}
