IF OBJECT_ID('dbo.automatically_check_objective_answers_trigger') IS NOT NULL
DROP TRIGGER dbo.automatically_check_objective_answers_trigger;

GO

CREATE TRIGGER dbo.automatically_check_objective_answers_trigger
ON dbo.AnswersByInterviewees
AFTER INSERT
AS


BEGIN
    DECLARE @AnswerId				        bigint;
    DECLARE @IntervieweeId				    bigint;
    DECLARE @SetQuestionId					bigint;
    DECLARE @ObjectiveAnswer				national character varying(1000);

	DECLARE @CorrectAnswerCount int
	DECLARE @AnswersByIntervieweeCount int
	DECLARE @MarksbyEachCorrectOption decimal(5,2)
	DECLARE @MarksObtained decimal(5,2)


    SELECT
     @AnswerId				       = AnswerId,
     @IntervieweeId				   = IntervieweeId,
     @SetQuestionId				   = SetQuestionId,
	 @ObjectiveAnswer			   = ObjectiveAnswer
FROM INSERTED;


print @AnswerId;
SELECT 
	InterviewQuestions.QuestionId,
	InterviewQuestions.Marks,
	InterviewQuestions.QuestionTypeId,
	InterviewQuestions.JobTitle,
	InterviewQuestions.SetQuestionId,
	InterviewQuestions.Question,
	ObjectiveQuestionOptions.ObjectiveQuestionOptionId,
	ObjectiveQuestionOptions.AnswerOption,
	ObjectiveQuestionOptions.IsAnswer
INTO ##InterviewQuestionswithOptions
FROM InterviewQuestions
INNER JOIN ObjectiveQuestionOptions
ON InterviewQuestions.QuestionId = ObjectiveQuestionOptions.QuestionId
WHERE InterviewQuestions.IntervieweeId = @IntervieweeId
AND InterviewQuestions.SetQuestionId = @SetQuestionId
AND ObjectiveQuestionOptions.IsAnswer = 1

-- getting count of correct answers
SELECT @CorrectAnswerCount = count(*) FROM ##InterviewQuestionswithOptions;

--getting marks carred by each correct option
--total marks of question divided by total number of correct answers
-- eg questionMark = 10, correctAnswerCount = 2 so, markofEachCorrectOption = 10/2 = 5;
SET @MarksbyEachCorrectOption = (SELECT DISTINCT Marks/@CorrectAnswerCount FROM  ##InterviewQuestionswithOptions) 

-- getting objective answers by interviewees in single column eg 1,2,3 is splitted into 3 rows of single column
SELECT LTRIM(RTRIM(Split.a.value('.', 'VARCHAR(100)'))) 'Answers' 
	INTO ##AnswersByInterviewee
	FROM  
	(     
		 SELECT CAST ('<M>' + REPLACE(@ObjectiveAnswer,',', '</M><M>') + '</M>' AS XML) AS Data            
	) AS A 
CROSS APPLY Data.nodes ('/M') AS Split(a)

-- counting objective answers answered by interviewee
SELECT @AnswersByIntervieweeCount = COUNT(*) FROM ##AnswersByInterviewee;

-- checking condition
-- if interviewee has checked answers greater than the count of correct answer then the answer is invalid
-- total correct options = 2 and interviewee has checked 3 options then the answer is invalid resulting 0 marks.

IF(@AnswersByIntervieweeCount > @CorrectAnswerCount)
BEGIN
--invalid answer
INSERT INTO dbo.Results
(AnswerId,MarksObtained,Remarks,CheckedBy,AuditTs,Deleted)
VALUES (@AnswerId,0,'INVALID ANSWER',1,GETDATE(),0);
END
ELSE
BEGIN
SELECT @MarksObtained = COUNT(*) * @MarksbyEachCorrectOption  FROM ##InterviewQuestionswithOptions
WHERE ##InterviewQuestionswithOptions.ObjectiveQuestionOptionId IN (SELECT * FROM ##AnswersByInterviewee)

INSERT INTO dbo.Results
(AnswerId,MarksObtained,CheckedBy,AuditTs,Deleted)
VALUES (@AnswerId,@MarksObtained,1,GETDATE(),0);
END

DROP TABLE ##InterviewQuestionswithOptions
DROP TABLE ##AnswersByInterviewee;

END