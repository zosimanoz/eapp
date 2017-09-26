CREATE VIEW SessionJobView
AS
SELECT 
	SessionwiseJobs.SessionwiseJobId,
	SessionwiseJobs.JobTitleId,
	SessionwiseJobs.InterviewSessionId,
	ExamSets.ExamSetId,
	ExamSets.Title AS ExamSetTitle,
	JobTitles.JobTitle
FROM SessionwiseJobs
INNER JOIN ExamSets
ON SessionwiseJobs.ExamSetId = ExamSets.ExamSetId
INNER JOIN JobTitles
ON SessionwiseJobs.JobTitleId = JobTitles.JobTitleId
WHERE SessionwiseJobs.Deleted = 0;
GO
