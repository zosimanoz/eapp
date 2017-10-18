CREATE VIEW InterviewQuestionsToCheckAnswersView
AS
SELECT
	InterviewQuestions.QuestionId ,
	InterviewQuestions.IntervieweeId,
	InterviewQuestions.Attachment,
	InterviewQuestions.Marks,
	InterviewQuestions.Question,
	InterviewQuestions.SetQuestionId,
	InterviewQuestions.QuestionTypeId,
	AnswersByInterviewees.AnswerId,
	AnswersByInterviewees.subjectiveAnswer,
	Results.ResultId,
	Results.MarksObtained,
	Results.Remarks
FROM InterviewQuestions
INNER JOIN AnswersByInterviewees 
ON InterviewQuestions.SetQuestionId = AnswersByInterviewees.SetQuestionId
AND InterviewQuestions.IntervieweeId = AnswersByInterviewees.IntervieweeId
LEFT JOIN Results 
ON Results.AnswerId = AnswersByInterviewees.AnswerId