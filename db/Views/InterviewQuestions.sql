CREATE VIEW InterviewQuestions
AS
SELECT 
 dbo.QuestionBank.QuestionId,
 dbo.QuestionBank.Question,
 dbo.QuestionBank.QuestionTypeId,
 dbo.QuestionBank.Attachment,
 dbo.QuestionBank.Marks,
 dbo.Interviewees.IntervieweeId
 FROM dbo.Interviewees
INNER JOIN dbo.SessionWiseJobs
ON Interviewees.InterviewSessionId = dbo.SessionwiseJobs.InterviewSessionId
AND Interviewees.JobTitleId = dbo.SessionwiseJobs.JobTitleId
INNER JOIN dbo.SetQuestions
ON dbo.SetQuestions.QuestionSetId = dbo.SessionwiseJobs.QuestionSetId
INNER JOIN dbo.QuestionBank
ON dbo.QuestionBank.QuestionId = dbo.SetQuestions.QuestionId;





