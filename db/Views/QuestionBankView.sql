CREATE VIEW QuestionBankView
AS
SELECT 
	QuestionBank.QuestionId,
	QuestionBank.QuestionTypeId,
	QuestionTypes.TypeName AS QuestionTypeName,
	QuestionTypes.TypeCode AS QuestionTypeCode,
	QuestionBank.QuestionCategoryId,
	QuestionCategories.CategoryCode AS QuestionCategoryCode,
	QuestionCategories.CategoryName AS QuestionCategoryName,
	QuestionBank.Question,
	QuestionBank.Attachment,
	QuestionBank.QuestionComplexityId,
	QuestionComplexities.ComplexityTitle AS QuestionComplexityName,
	QuestionComplexities.ComplexityCode AS QuestionComplexityCode,
	QuestionBank.Marks
FROM QuestionBank
INNER JOIN QuestionTypes 
ON QuestionTypes.QuestionTypeId = QuestionBank.QuestionTypeId
INNER JOIN QuestionCategories
ON QuestionCategories.QuestionCategoryId = QuestionBank.QuestionCategoryId
INNER JOIN QuestionComplexities
ON QuestionComplexities.QuestionComplexityId = QuestionBank.QuestionComplexityId
WHERE QuestionBank.Deleted = 0;
