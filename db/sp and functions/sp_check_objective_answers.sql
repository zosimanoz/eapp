USE [VExam]
GO
/****** Object:  StoredProcedure [dbo].[sp_check_objective_answers]    Script Date: 8/15/2017 11:38:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE dbo.sp_check_objective_answers
	@intervieweeId bigint
AS
BEGIN
	DECLARE @QuestionId bigint
	DECLARE @SetQuestionId bigint
	DECLARE @QuestionTypeId int
	DECLARE @Marks decimal(5,2)
	DECLARE @CorrectAnswerCount int
	DECLARE @AnswersByIntervieweeCount int
	DECLARE @Answers nvarchar(1000) 
	DECLARE @MarksbyEachCorrectOption decimal(5,2)
	DECLARE @MarksObtained decimal(5,2)
	DECLARE @AnswerId bigint

	--Declare @intervieweeId bigint = 3

	DECLARE cur CURSOR FOR 
		SELECT 
			InterviewQuestions.QuestionId,
			InterviewQuestions.SetQuestionId,
			InterviewQuestions.QuestionTypeId,
			InterviewQuestions.Marks
	FROM InterviewQuestions WHERE IntervieweeId = @intervieweeId
	OPEN cur

	FETCH NEXT FROM cur INTO @QuestionId, @SetQuestionId,@QuestionTypeId,@Marks

	WHILE @@FETCH_STATUS = 0 BEGIN
	DECLARE @IsChecked bit = (SELECT IsChecked FROM AnswersByInterviewees WHERE SetQuestionId = @SetQuestionId);

	IF (@QuestionTypeId = '2' AND @IsChecked = 0)
	BEGIN

	--getting objective answers and answer id from AnswersByInterviewees
	SELECT 
		@Answers = ObjectiveAnswer,
		@AnswerId =  AnswerId
	FROM AnswersByInterviewees 
	WHERE SetQuestionId = @SetQuestionId;

	-- getting count of correct answers for current question
	SELECT 
		@CorrectAnswerCount = count(*) 
	FROM ObjectiveQuestionOptions 
	WHERE ObjectiveQuestionOptions.QuestionId=@QuestionId
	AND ObjectiveQuestionOptions.IsAnswer = 1;

	--getting marks carred by each correct option
	--total marks of question divided by total number of correct answers
	-- eg questionMark = 10, correctAnswerCount = 2 so, markofEachCorrectOption = 10/2 = 5;

	SET @MarksbyEachCorrectOption = @Marks/@CorrectAnswerCount 


	-- getting objective answers by interviewees in single column eg 1,2,3 is splitted into 3 rows of single column

	SELECT LTRIM(RTRIM(Split.a.value('.', 'VARCHAR(100)'))) 'Answers' 
		INTO #AnswersByInterviewee
		FROM  
		(     
			 SELECT CAST ('<M>' + REPLACE(@Answers,',', '</M><M>') + '</M>' AS XML) AS Data            
		) AS A 
	CROSS APPLY Data.nodes ('/M') AS Split(a)

	-- counting objective answers answered by interviewee
	SELECT @AnswersByIntervieweeCount = COUNT(*) FROM #AnswersByInterviewee;

	-- checking condition
	-- if interviewee has checked answers greater than the count of correct answer then the answer is invalid
	-- total correct options = 2 and interviewee has checked 3 options then the answer is invalid resulting 0 marks.

	IF(@AnswersByIntervieweeCount > @CorrectAnswerCount)
		BEGIN
			--invalid answer
			INSERT INTO dbo.Results
			(AnswerId,MarksObtained,Remarks,CheckedBy,AuditTs,Deleted)
			VALUES (@AnswerId,0,'INVALID ANSWER',1,GETDATE(),0);

			UPDATE AnswersByInterviewees
			SET IsChecked = 1
			WHERE AnswerId = @AnswerId
		END
		ELSE
			BEGIN
				SELECT @MarksObtained = COUNT(*) * @MarksbyEachCorrectOption  
				FROM ObjectiveQuestionOptions
				WHERE ObjectiveQuestionOptions.ObjectiveQuestionOptionId 
				IN (SELECT * FROM #AnswersByInterviewee)
				AND ObjectiveQuestionOptions.QuestionId=@QuestionId
				AND ObjectiveQuestionOptions.IsAnswer = 1;

				INSERT INTO dbo.Results
				(AnswerId,MarksObtained,CheckedBy,AuditTs,Deleted)
				VALUES (@AnswerId,@MarksObtained,1,GETDATE(),0);

				UPDATE AnswersByInterviewees
				SET IsChecked = 1
				WHERE AnswerId = @AnswerId

			END

			DROP TABLE #AnswersByInterviewee;
		END
 
    FETCH NEXT FROM cur INTO @QuestionId, @SetQuestionId,@QuestionTypeId,@Marks
END

CLOSE cur    
DEALLOCATE cur

END
