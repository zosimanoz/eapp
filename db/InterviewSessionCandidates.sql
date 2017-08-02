CREATE VIEW dbo.InterviewSessionCandidate_view
AS
SELECT 
	Interviewees.IntervieweeId,
	Interviewees.EmailAddress,
	Interviewees.InterviewSessionId,
	Interviewees.FirstName,
	Interviewees.MiddleName,
	Interviewees.LastName,
	Interviewees.Address,
	Interviewees.ContactNumber,
	Interviewees.Attachments,
	InterviewSessions.Title AS SessionTitle,
	JobTitles.JobTitle
FROM interviewees
INNER JOIN InterviewSessions
ON interviewees.InterviewSessionId = InterviewSessions.InterviewSessionId
INNER JOIN JobTitles
ON JobTitles.JobTitleId = InterviewSessions.JobTitleId
WHERE InterviewSessions.SessionStartDate <= GetDate()
AND InterviewSessions.SessionEndDate >= GetDate()
AND InterviewSessions.Deleted = 0
AND interviewees.Deleted = 0;


