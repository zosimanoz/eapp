CREATE VIEW QuestionsForSetView
As
SELECT 
	SetQuestions.SetQuestionId,
	SetQuestions.ExamSetId,
	QuestionBank.QuestionId,
	QuestionBank.QuestionTypeId,
	QuestionTypes.TypeName AS QuestionTypeName,
	QuestionTypes.TypeCode AS QuestionTypeCode,
	QuestionBank.QuestionCategoryId,
	QuestionCategories.CategoryCode AS QuestionCategoryCode,
	QuestionCategories.CategoryName AS QuestionCategoryName,
	QuestionBank.JobTitleId,
	JobTitles.JobTitle,
	QuestionBank.Question,
	QuestionBank.Attachment,
	QuestionBank.QuestionComplexityId,
	QuestionComplexities.ComplexityTitle AS QuestionComplexityName,
	QuestionComplexities.ComplexityCode AS QuestionComplexityCode
FROM SetQuestions
INNER JOIN QuestionBank
ON QuestionBank.QuestionId = SetQuestions.QuestionId
INNER JOIN QuestionTypes 
ON QuestionTypes.QuestionTypeId = QuestionBank.QuestionTypeId
INNER JOIN QuestionCategories
ON QuestionCategories.QuestionCategoryId = QuestionBank.QuestionCategoryId
INNER JOIN JobTitles
ON JobTitles.JobTitleId = QuestionBank.JobTitleId
INNER JOIN QuestionComplexities
ON QuestionComplexities.QuestionComplexityId = QuestionBank.QuestionComplexityId
WHERE SetQuestions.Deleted = 0







