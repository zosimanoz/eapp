CREATE VIEW dbo.InterviewSessionCandidate_view
AS
SELECT 
	Interviewees.IntervieweeId,
	Interviewees.EmailAddress,
	Interviewees.InterviewSessionId,
	Interviewees.FirstName,
	Interviewees.MiddleName,
	Interviewees.LastName,
	LTRIM(Interviewees.FirstName) +' '+ LTRIM(LTRIM(Interviewees.MiddleName) +' '+ LTRIM(Interviewees.LastName)) AS FullName,
	Interviewees.Address,
	Interviewees.ContactNumber,
	Interviewees.Attachments,
	InterviewSessions.Title AS SessionTitle,
	JobTitles.JobTitle,
	JobTitles.JobTitleId
FROM interviewees
INNER JOIN InterviewSessions
ON interviewees.InterviewSessionId = InterviewSessions.InterviewSessionId
INNER JOIN JobTitles
ON JobTitles.JobTitleId = interviewees.JobTitleId
WHERE InterviewSessions.SessionStartDate <= GetDate()
AND InterviewSessions.SessionEndDate >= GetDate()
AND InterviewSessions.Deleted = 0
AND interviewees.Deleted = 0;





