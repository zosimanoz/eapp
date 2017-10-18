CREATE Procedure ObjectiveIptionsToCheckAnswersProcedure
AS
BEGIN
	DECLARE @QuestionId bigint
	DECLARE @SetQuestionId bigint
	DECLARE @QuestionTypeId int
	DECLARE @Answers nvarchar(1000) 
	DECLARE @AnswerId bigint
	
	CREATE TABLE #tmpObjectiveOptionWithAnswerByInterviewee
	(
		ObjectiveQuestionOptionId bigint,
		QuestionId bigint,
		AnswerOption nvarchar(500),
		Attachment nvarchar(500),
		IsAnswer bit,
		Answered bit
	)


	DECLARE cur CURSOR FOR 
		SELECT 
			InterviewQuestions.QuestionId,
			InterviewQuestions.SetQuestionId,
			InterviewQuestions.QuestionTypeId
	FROM InterviewQuestions
	OPEN cur

	FETCH NEXT FROM cur INTO @QuestionId, @SetQuestionId,@QuestionTypeId

	WHILE @@FETCH_STATUS = 0 BEGIN
		
	BEGIN

	--getting objective answers and answer id from AnswersByInterviewees
	SELECT 
		@Answers = ObjectiveAnswer,
		@AnswerId =  AnswerId
	FROM AnswersByInterviewees 
	WHERE SetQuestionId = @SetQuestionId;

	-- getting objective answers by interviewees in single column eg 1,2,3 is splitted into 3 rows of single column
	
	SELECT LTRIM(RTRIM(Split.a.value('.', 'VARCHAR(100)'))) 'Answers', CAST(1 AS bit) as Status
		INTO #AnswersByInterviewee
		FROM  
		(     
			 SELECT CAST ('<M>' + REPLACE(@Answers,',', '</M><M>') + '</M>' AS XML) AS Data            
		) AS A 
	CROSS APPLY Data.nodes ('/M') AS Split(a)

	INSERT INTO #tmpObjectiveOptionWithAnswerByInterviewee
		SELECT 
			ObjectiveQuestionOptions.ObjectiveQuestionOptionId,
			ObjectiveQuestionOptions.QuestionId,
			ObjectiveQuestionOptions.AnswerOption,
			ObjectiveQuestionOptions.Attachment,
			ObjectiveQuestionOptions.IsAnswer,
			COALESCE(#AnswersByInterviewee.Status, 0)
		 FROM ObjectiveQuestionOptions 
		 left join #AnswersByInterviewee
		 on ObjectiveQuestionOptions.ObjectiveQuestionOptionId = #AnswersByInterviewee.Answers
		 WHERE ObjectiveQuestionOptions.QuestionId = @QuestionId
	
		END
 drop table #AnswersByInterviewee;
    FETCH NEXT FROM cur INTO @QuestionId, @SetQuestionId,@QuestionTypeId
END

CLOSE cur    
DEALLOCATE cur
select * from #tmpObjectiveOptionWithAnswerByInterviewee;

DROP TABLE #tmpObjectiveOptionWithAnswerByInterviewee;

END



