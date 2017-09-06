CREATE VIEW InterviewQuestions
AS
SELECT 
	 dbo.QuestionBankView.QuestionId,
	 dbo.QuestionBankView.Question,
	 dbo.QuestionBankView.QuestionTypeId,
	 dbo.QuestionBankView.Attachment,
	 dbo.QuestionBankView.Marks,
	 dbo.QuestionBankView.QuestionCategoryCode,
	 dbo.QuestionBankView.QuestionCategoryId,
	 dbo.QuestionBankView.QuestionCategoryName,
	 dbo.QuestionBankView.QuestionComplexityCode,
	 dbo.QuestionBankView.QuestionComplexityId,
	 dbo.QuestionBankView.QuestionComplexityName,
	 dbo.QuestionBankView.QuestionTypeCode,
	 dbo.QuestionBankView.QuestionTypeName,
	 dbo.Interviewees.IntervieweeId,
	 dbo.SetQuestions.SetQuestionId
 FROM dbo.Interviewees
INNER JOIN dbo.SessionWiseJobs
ON Interviewees.InterviewSessionId = dbo.SessionwiseJobs.InterviewSessionId
AND Interviewees.JobTitleId = dbo.SessionwiseJobs.JobTitleId
INNER JOIN dbo.SetQuestions
ON dbo.SetQuestions.ExamSetId = dbo.SessionwiseJobs.ExamSetId
INNER JOIN dbo.QuestionBankView
ON dbo.QuestionBankView.QuestionId = dbo.SetQuestions.QuestionId
WHERE dbo.Interviewees.deleted = 0
AND dbo.SetQuestions.Deleted = 0;

