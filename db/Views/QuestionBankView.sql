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
QuestionBank.JobTitleId,
JobTitles.JobTitle,
QuestionBank.Question,
QuestionBank.Attachment,
QuestionBank.QuestionComplexityId,
QuestionComplexities.ComplexityTitle AS QuestionComplexityName,
QuestionComplexities.ComplexityCode AS QuestionComplexityCode
FROM QuestionBank
INNER JOIN QuestionTypes 
ON QuestionTypes.QuestionTypeId = QuestionBank.QuestionTypeId
INNER JOIN QuestionCategories
ON QuestionCategories.QuestionCategoryId = QuestionCategories.QuestionCategoryId
INNER JOIN JobTitles
ON JobTitles.JobTitleId = QuestionBank.JobTitleId
INNER JOIN QuestionComplexities
ON QuestionComplexities.QuestionComplexityId = QuestionBank.QuestionComplexityId
WHERE QuestionBank.Deleted = 0;
