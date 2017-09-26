CREATE VIEW [dbo].[InterviewSessionHistoryView]
AS
SELECT 
	InterviewSessions.InterviewSessionId,
	InterviewSessions.Title,
	InterviewSessions.SessionStartDate,
	InterviewSessions.SessionEndDate
FROM  InterviewSessions
WHERE NOT (InterviewSessions.SessionStartDate <= GetDate()
AND InterviewSessions.SessionEndDate >= GetDate())
AND InterviewSessions.Deleted = 0;



