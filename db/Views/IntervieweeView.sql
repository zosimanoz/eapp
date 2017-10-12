CREATE VIEW IntervieweeView
AS
SELECT  
	interviewees.IntervieweeId,
	interviewees.EmailAddress,
	interviewees.InterviewSessionId,
	interviewees.FirstName,
	interviewees.MiddleName,
	interviewees.LastName,
	LTRIM(interviewees.FirstName)+' '+ LTRIM(LTRIM(interviewees.MiddleName)+' ' + LTRIM(interviewees.LastName)) AS FullName,
	interviewees.Address,
	interviewees.ContactNumber,
	interviewees.Attachments,
	Interviewees.AttendedExam,
	interviewees.JobTitleId,
	JobTitles.JobTitle
FROM interviewees
INNER JOIN JobTitles
ON Interviewees.JobTitleId = JobTitles.JobTitleId
WHERE interviewees.Deleted = 0
AND JobTitles.Deleted =0