namespace VExam.DTO
{
    public class ResultSummary
    {
        public long IntervieweeId { get; set; }
        public long InterviewSessionId { get; set; }
        public string IntervieweeName { get; set; }
        public string JobTitle { get; set; }
        public int JobTitleId { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
        public decimal MarksObtained { get; set; }

    }
}