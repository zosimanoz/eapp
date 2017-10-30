ALTER VIEW ExamSetForIntervieweeView
AS
SELECT 
	ExamSets.ExamSetId,
	ExamSets.Title,
	ExamSets.Description,
	ExamSets.JobTitleId,
	JobTitles.JobTitle,
	ExamSets.ExamDuration,
	ExamSets.TotalMark,
	Interviewees.IntervieweeId
FROM dbo.Interviewees
INNER JOIN dbo.SessionWiseJobs
ON Interviewees.InterviewSessionId = dbo.SessionwiseJobs.InterviewSessionId
AND Interviewees.JobTitleId = dbo.SessionwiseJobs.JobTitleId
INNER JOIN dbo.ExamSets
ON dbo.ExamSets.ExamSetId = dbo.SessionwiseJobs.ExamSetId
INNER JOIN dbo.JobTitles 
ON JobTitles.JobTitleId = dbo.SessionwiseJobs.JobTitleId
WHERE dbo.Interviewees.deleted = 0
AND SessionWiseJobs.Deleted = 0;