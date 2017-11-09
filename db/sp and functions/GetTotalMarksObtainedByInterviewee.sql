CREATE FUNCTION [dbo].[GetTotalMarksObtainedByInterviewee] (
     @intervieweeId bigint )
RETURNS decimal(5,2) 
AS 
	BEGIN 
		DECLARE @TotalMarksObtained decimal(5,2);  
		SELECT
			@TotalMarksObtained= SUM(ISNULL(Results.MarksObtained,0)) FROM InterviewQuestions 
		INNER JOIN AnswersByInterviewees
		ON InterviewQuestions.SetQuestionId = AnswersByInterviewees.SetQuestionId
		INNER JOIN Results 
		ON AnswersByInterviewees.AnswerId = Results.AnswerId
		WHERE InterviewQuestions.IntervieweeId = @intervieweeId
		AND AnswersByInterviewees.IsChecked = 1
		RETURN @TotalMarksObtained 
	END

GO


