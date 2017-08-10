CREATE VIEW ActiveInterviewSessionView
AS
SELECT 
	InterviewSessions.InterviewSessionId,
	InterviewSessions.Title,
	InterviewSessions.SessionStartDate,
	InterviewSessions.SessionEndDate,
	InterviewSessions.JobTitleId,
	JobTitles.JobTitle
FROM  InterviewSessions
INNER JOIN JobTitles
ON JobTitles.JobTitleId = InterviewSessions.JobTitleId
WHERE InterviewSessions.SessionStartDate <= GetDate()
AND InterviewSessions.SessionEndDate >= GetDate()
AND InterviewSessions.Deleted = 0;
