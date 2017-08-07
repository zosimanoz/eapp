CREATE TABLE dbo.Roles
(
	RoleId									int IDENTITY PRIMARY KEY,
	Title									national character varying(500),
	Description								national character varying(1000)
);

CREATE TABLE dbo.Departments
(
	DepartmentId							int IDENTITY PRIMARY KEY,
	DepartmentCode							national character varying(50),
	DepartmentName							national character varying(200),
	Deleted									bit NOT NULL DEFAULT(0)
);

CREATE TABLE dbo.Users
(
	UserId									bigint IDENTITY PRIMARY key,
	RoleId									national character varying(100) NOT NULL,
	FirstName								national character varying(200) NOT NULL,
	MiddleName								national character varying (200),
	LastName								national character varying(200) NOT NULL,
	DepartmentId							int REFERENCES dbo.Departments NOT NULL,
	EmailAddress							national character varying (100) NOT NULL,
	Password 								national character varying (100) NOT NULL,
	PasswordChanged							bit NOT NULL DEFAULT(0),
	Deleted									bit NOT NULL DEFAULT(0)
);


CREATE TABLE dbo.JobTitles
(
	JobTitleId								int IDENTITY PRIMARY KEY,
	JobTitle 								national character varying(200) NOT NULL,
	Description								national character varying(500),
	Deleted									bit NOT NULL DEFAULT(0)								
);

CREATE TABLE dbo.QuestionTypes  -- subjective or objective Question
(
	QuestionTypeId							int IDENTITY PRIMARY KEY,
	TypeCode								national character varying(50) NOT NULL,
	TypeName								national character varying(200) NOT NULL,
	Deleted									bit NOT NULL DEFAULT(0) 
);

CREATE TABLE dbo.QuestionCategories  --  categories like C#, javascript, I.Q etc.
(
	QuestionCategoryId  					int IDENTITY PRIMARY KEY,
	CategoryCode							national character varying(50) NOT NULL,
	CategoryName							national character varying(200) NOT NULL,
	Description								national character varying(1000),
	Deleted									bit NOT NULL DEFAULT(0)  
);

CREATE TABLE dbo.QuestionComplexities
(
	QuestionComplexityId					int IDENTITY PRIMARY KEY,
	ComplexityCode 							national character varying(50) NOT NULL,
	ComplexityTitle 						national character varying(100) NOT NULL,
	Deleted									bit NOT NULL DEFAULT(0) 

);


CREATE TABLE dbo.QuestionBank
(
	QuestionId 								bigint IDENTITY PRIMARY KEY,
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
	ObjectiveQuestionOptionId				bigint IDENTITY PRIMARY key,
	QuestionId 								bigint REFERENCES dbo.QuestionBank NOT NULL,
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

CREATE TABLE dbo.ExamSets
(
	ExamSetId								bigint IDENTITY PRIMARY KEY,
	Title									national character varying(1000) NOT NULL,
	JobTitleId								int REFERENCES JobTitles NOT NULL,
	CreatedBy 								bigint REFERENCES dbo.Users NOT NULL,
	AuditTs 								DATETIMEOFFSET DEFAULT(GETDATE()),
	Deleted									bit NOT NULL DEFAULT(0) 
);

CREATE TABLE dbo.SetQuestions -- sets should not be randomized for interviewees...
(
	SetQuestionId							bigint IDENTITY PRIMARY KEY,
	ExamSetId								bigint REFERENCES dbo.ExamSets NOT NULL,
	QuestionId								bigint REFERENCES dbo.QuestionBank NOT NULL,
	CreatedBy 								bigint REFERENCES dbo.Users NOT NULL,
	AuditTs 								DATETIMEOFFSET DEFAULT(GETDATE()),
	Deleted									bit NOT NULL DEFAULT(0) 
);

CREATE TABLE dbo.InterviewSessions
(
	InterviewSessionId						bigint IDENTITY PRIMARY KEY,
	Title 									national character varying(500),
	SessionStartDate						DATETIMEOFFSET,
	SessionEndDate							DATETIMEOFFSET,
	CreatedBy 								bigint REFERENCES dbo.Users NOT NULL,
	AuditTs 								DATETIMEOFFSET DEFAULT(GETDATE()),
	Deleted									bit NOT NULL DEFAULT(0) 

);

CREATE TABLE dbo.SessionwiseJobs
(
	SessionwiseJobs							bigint IDENTITY PRIMARY KEY,
	InterviewSessionId						bigint REFERENCES dbo.InterviewSessions NOT NULL,
	JobTitleId								int REFERENCES dbo.JobTitles NOT NULL,
	ExamSetId								bigint REFERENCES dbo.ExamSets NOT NULL,
	CreatedBy 								bigint REFERENCES dbo.Users NOT NULL,
	AuditTs 								DATETIMEOFFSET DEFAULT(GETDATE()),
	Deleted									bit NOT NULL DEFAULT(0) 
);

CREATE TABLE dbo.Interviewees
(
	IntervieweeId							bigint IDENTITY PRIMARY key,
	EmailAddress							national character varying (100) NOT NULL,
	InterviewSessionId						bigint REFERENCES dbo.InterviewSessions NOT NULL,
	JobTitleId								int REFERENCES dbo.JobTitles NOT NULL,
	FirstName								national character varying(200) NOT NULL,
	MiddleName								national character varying (200),
	LastName								national character varying(200) NOT NULL,
	Address 								national character varying(200) NOT NULL,
	ContactNumber							national character varying(200) NOT NULL,
	Attachments								national character varying(1000),
	AttendedExam							bit NOT NULL DEFAULT(0),
	Deleted									bit NOT NULL DEFAULT(0)
);



-- CREATE TABLE dbo.InterviewSessionQuestions
-- (
	-- InterviewSessionQuestionId				bigint PRIMARY KEY,
	-- InterviewSessionId  					bigint REFERENCES dbo.InterviewSessions,
	-- QuestionId 								bigint REFERENCES dbo.Questions,
	-- CreatedBy 								bigint REFERENCES dbo.users NOT NULL,
	-- AuditTs 								DATETIMEOFFSET DEFAULT(GETDATE()),
	-- Deleted									bit NOT NULL DEFAULT(0) 
-- );

-- CREATE TABLE dbo.InterviewSessionInterviewees
-- (
	-- InterviewSessionId						bigint REFERENCES dbo.InterviewSessions NOT NULL,
	-- IntervieweeId							bigint REFERENCES dbo.Interviewees,
	-- CreatedBy 								bigint REFERENCES dbo.users NOT NULL,
	-- AuditTs 								DATETIMEOFFSET DEFAULT(GETDATE()),
	-- Deleted									bit NOT NULL DEFAULT(0) 
-- );

CREATE TABLE dbo.AnswersByInterviewees
(
	AnswerId 								bigint IDENTITY PRIMARY key,
	IntervieweeId 							bigint REFERENCES dbo.Interviewees NOT NULL,
	SetQuestionId 							bigint REFERENCES dbo.SetQuestions NOT NULL,
	subjectiveAnswer						national character varying(MAX),
	ObjectiveAnswer							national character varying(1000),
	AnsweredBy								bigint REFERENCES dbo.Interviewees,
	AuditTs				 					DATETIMEOFFSET DEFAULT(GETDATE()),
	Deleted									bit NOT NULL DEFAULT(0) 
);

CREATE TABLE dbo.CheckedBy
(
	CheckedById								int PRIMARY key,
	Title									national character varying(500),
	Deleted									bit NOT NULL DEFAULT(0)
	
);

INSERT INTO dbo.CheckedBy Values
(1,'Automatic by System',0);
INSERT INTO dbo.CheckedBy Values
(2,'Examiner',0);


CREATE TABLE dbo.Results
(
	ResultId								bigint PRIMARY KEY,
	AnswerId 								bigint REFERENCES dbo.AnswersByInterviewees NOT NULL,
	MarksObtained							decimal NOT NULL,
	Remarks									national character varying(500),
	CheckedBy 								int REFERENCES dbo.CheckedBy NOT NULL,
	ExaminerId								bigint REFERENCES dbo.users,
	AuditTs									DATETIMEOFFSET DEFAULT(GETDATE()),
	Deleted									bit NOT NULL DEFAULT(0) 
);



