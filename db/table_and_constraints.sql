CREATE TABLE dbo.Roles
(
	RoleId									int PRIMARY KEY,
	Title									national character varying(500),
	Description								national character varying(1000)
);

CREATE TABLE dbo.Departments
(
	DepartmentId							int PRIMARY KEY,
	DepartmentCode							national character varying(50),
	DepartmentName							national character varying(200),
	Deleted									bit NOT NULL DEFAULT(0)
);

CREATE TABLE dbo.Users
(
	UserId									bigint PRIMARY key,
	RoleId									int REFERENCES dbo.Roles NOT NULL,
	EmailAddress							national character varying (100),
	INumber 								national character varying (100) NOT NULL,
	FirstName								national character varying(200) NOT NULL,
	MiddleName								national character varying (200),
	LastName								national character varying(200) NOT NULL,
	Address 								national character varying(200) NOT NULL,
	ContactNumber							national character varying(200) NOT NULL,
	DepartmentId							int REFERENCES dbo.Departments,
	ProfilePicture							national character varying(1000),
	Deleted									bit NOT NULL DEFAULT(0)
);

CREATE TABLE dbo.Interviewees
(
	IntervieweeId							bigint PRIMARY key,
	EmailAddress							national character varying (100) NOT NULL,
	Password								national character varying (100) NOT NULL,
	FirstName								national character varying(200) NOT NULL,
	MiddleName								national character varying (200),
	LastName								national character varying(200) NOT NULL,
	Address 								national character varying(200) NOT NULL,
	ContactNumber							national character varying(200) NOT NULL,
	ProfilePicture							national character varying(1000),
	Deleted									bit NOT NULL DEFAULT(0)
);

CREATE TABLE dbo.JobTitles
(
	JobTitleId								int PRIMARY KEY,
	JobTitle 								national character varying(200) NOT NULL,
	Description								national character varying(500),
	Deleted									bit NOT NULL DEFAULT(0)								
);

CREATE TABLE dbo.QuestionTypes  -- subjective or objective Question
(
	QuestionTypeId							int PRIMARY KEY,
	TypeCode								national character varying(50) NOT NULL,
	TypeName								national character varying(200) NOT NULL,
	Deleted									bit NOT NULL DEFAULT(0) 
);

CREATE TABLE dbo.QuestionCategories  --  categories like C#, javascript, I.Q etc.
(
	QuestionCategoryId  					int PRIMARY KEY,
	CategoryCode							national character varying(50) NOT NULL,
	CategoryName							national character varying(200) NOT NULL,
	Description								national character varying(1000),
	Deleted									bit NOT NULL DEFAULT(0)  
);

CREATE TABLE dbo.QuestionComplexities
(
	QuestionComplexityId					int PRIMARY KEY,
	ComplexityCode 							national character varying(50) NOT NULL,
	ComplexityTitle 						national character varying(100) NOT NULL,
	Deleted									bit NOT NULL DEFAULT(0) 

);


CREATE TABLE dbo.Questions
(
	QuestionId 								bigint PRIMARY KEY,
	QuestionTypeId							int REFERENCES dbo.QuestionTypes NOT NULL,
	QuestionCategoryId  					int REFERENCES dbo.QuestionCategories NOT NULL,
	JobTitleId								int REFERENCES dbo.JobTitles,
	Question 								national character varying(MAX),
	Attachment								national character varying(1000),
	QuestionComplexityId 				    int REFERENCES dbo.QuestionComplexities NOT NULL,
	Marks 									decimal,
	PreparedBy 								bigint REFERENCES dbo.Users NOT NULL,
	AuditTs									DATETIMEOFFSET DEFAULT(GETDATE()),
	Deleted									bit NOT NULL DEFAULT(0) 
);


CREATE TABLE dbo.ObjectiveQuestionOptions
(
	ObjectiveQuestionOptionId				bigint PRIMARY key,
	QuestionId 								bigint REFERENCES dbo.Questions NOT NULL,
	AnswerOption							national character varying(MAX),
	Attachment								national character varying(1000),
	IsAnswer								bit,
	Deleted									bit NOT NULL DEFAULT(0)
);

-- CREATE TABLE dbo.subjective_Question_answers
-- (
-- 	subjective_Question_answer_id			bigint PRIMARY key,
-- 	QuestionId 							bigint REFERENCES dbo.Questions NOT NULL,
-- 	answer									national character varying(MAX),
-- 	Deleted									boolean NOT NULL DEFAULT(false)
-- );

CREATE TABLE dbo.InterviewSessions
(
	InterviewSessionId						bigint PRIMARY KEY,
	Title 									national character varying(500),
	SessionStartDate						DATETIMEOFFSET,
	SessionEndDate							DATETIMEOFFSET,
	JobTitleId								int REFERENCES dbo.JobTitles NOT NULL,
	CreatedBy 								bigint REFERENCES dbo.Users NOT NULL,
	AuditTs 								DATETIMEOFFSET DEFAULT(GETDATE()),
	Deleted									bit NOT NULL DEFAULT(0) 

);

CREATE TABLE dbo.InterviewSessionQuestions
(
	InterviewSessionQuestionId				bigint PRIMARY KEY,
	InterviewSessionId  					bigint REFERENCES dbo.InterviewSessions,
	QuestionId 								bigint REFERENCES dbo.Questions,
	CreatedBy 								bigint REFERENCES dbo.users NOT NULL,
	AuditTs 								DATETIMEOFFSET DEFAULT(GETDATE()),
	Deleted									bit NOT NULL DEFAULT(0) 
);

CREATE TABLE dbo.Exams
(
	IntervieweeId 							bigint REFERENCES dbo.Interviewees NOT NULL,
	InterviewSessionQuestionId				bigint REFERENCES dbo.InterviewSessionQuestions NOT NULL,
	subjectiveAnswer						national character varying(MAX),
	ObjectiveAnswer							national character varying(1000),
	MarksObtained							decimal,
	CheckedBy								bigint REFERENCES dbo.users,
	AuditTs				 					DATETIMEOFFSET DEFAULT(GETDATE()),
	Deleted									bit NOT NULL DEFAULT(0) 
);

CREATE TABLE dbo.Results
(
	ResultId								bigint PRIMARY KEY,
	InterviewSessionId  					bigint REFERENCES dbo.InterviewSessions,
	IntervieweeId							bigint REFERENCES dbo.Interviewees,
	TotalMarksObtained						decimal NOT NULL,
	Remarks									national character varying(500),
	AuditTs									DATETIMEOFFSET DEFAULT(GETDATE()),
	Deleted									bit NOT NULL DEFAULT(0) 
);



