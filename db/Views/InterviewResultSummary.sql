CREATE VIEW InterviewResultSummaryView
AS
SELECT 
	Interviewees.IntervieweeId,
	Interviewees.InterviewSessionId,
	Interviewees.EmailAddress,
	LTrim(LTrim(Interviewees.FirstName) +' '+ LTRIM(LTRIM(Interviewees.MiddleName)+ ' '+Interviewees.LastName)) AS IntervieweeName,
	JobTitles.JobTitle,
	JobTitles.JobTitleId,
	Interviewees.Address,
	Interviewees.ContactNumber,
	SUM(Results.MarksObtained) AS MarksObtained
FROM Interviewees
LEFT JOIN AnswersByInterviewees
ON AnswersByInterviewees.IntervieweeId = Interviewees.IntervieweeId
INNER JOIN Results
ON Results.AnswerId = AnswersByInterviewees.AnswerId
INNER JOIN JobTitles
ON JobTitles.JobTitleId = Interviewees.JobTitleId
WHERE Interviewees.Deleted = 0
AND AnswersByInterviewees.Deleted = 0
AND Results.Deleted = 0
GROUP BY 
	Interviewees.IntervieweeId,
	Interviewees.InterviewSessionId,
	Interviewees.EmailAddress,
	Interviewees.FirstName,
	Interviewees.MiddleName,
	Interviewees.LastName,
	JobTitles.JobTitle,
	Interviewees.Address,
	Interviewees.ContactNumber,
	JobTitles.JobTitleId

	

	

	