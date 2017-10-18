CREATE VIEW ObjectiveOptionsWithAnswersByIntervieweesView
AS
WITH ObjectiveAnswersByInterviewees
AS
(
SELECT SetQuestionId,
LTRIM(RTRIM(m.n.value('.[1]','varchar(8000)'))) AS ObjectiveAnswer,CAST(1 AS bit) as Status
FROM
(
SELECT AnswersByInterviewees.SetQuestionId,CAST('<XMLRoot><RowData>' + REPLACE(AnswersByInterviewees.ObjectiveAnswer,',','</RowData><RowData>') + '</RowData></XMLRoot>' AS XML) AS x
FROM   AnswersByInterviewees
INNER JOIN InterviewQuestions
ON AnswersByInterviewees.SetQuestionId = InterviewQuestions.SetQuestionId
AND InterviewQuestions.QuestionTypeId = 2
)t
CROSS APPLY x.nodes('/XMLRoot/RowData')m(n)
)
SELECT 
	ObjectiveQuestionOptions.ObjectiveQuestionOptionId,
	ObjectiveQuestionOptions.QuestionId,
	ObjectiveQuestionOptions.AnswerOption,
	ObjectiveQuestionOptions.Attachment,
	ObjectiveQuestionOptions.IsAnswer,
	COALESCE(ObjectiveAnswersByInterviewees.Status, CAST(0 AS bit)) AS AnswerByInterviewees
	
FROM ObjectiveQuestionOptions 
left join ObjectiveAnswersByInterviewees
on ObjectiveQuestionOptions.ObjectiveQuestionOptionId = ObjectiveAnswersByInterviewees.ObjectiveAnswer
INNER JOIN InterviewQuestions
ON InterviewQuestions.QuestionId = ObjectiveQuestionOptions.QuestionId
WHERE ObjectiveQuestionOptions.Deleted = 0