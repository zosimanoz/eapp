USE [master]
GO
/****** Object:  Database [VExam]    Script Date: 10/25/2017 4:17:41 PM ******/
CREATE DATABASE [VExam]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VExam', FILENAME = N'E:\MSSQL_Temp\Data\VExam.mdf' , SIZE = 3520KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'VExam_log', FILENAME = N'E:\MSSQL_Temp\Log\VExam.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [VExam] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VExam].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VExam] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [VExam] SET ANSI_NULLS ON 
GO
ALTER DATABASE [VExam] SET ANSI_PADDING ON 
GO
ALTER DATABASE [VExam] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [VExam] SET ARITHABORT ON 
GO
ALTER DATABASE [VExam] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VExam] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VExam] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VExam] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VExam] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [VExam] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [VExam] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VExam] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [VExam] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VExam] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VExam] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VExam] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VExam] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VExam] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VExam] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VExam] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VExam] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VExam] SET RECOVERY FULL 
GO
ALTER DATABASE [VExam] SET  MULTI_USER 
GO
ALTER DATABASE [VExam] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VExam] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VExam] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VExam] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [VExam] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'VExam', N'ON'
GO
USE [VExam]
GO
/****** Object:  Table [dbo].[AnswersByInterviewees]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnswersByInterviewees](
	[AnswerId] [bigint] IDENTITY(1,1) NOT NULL,
	[IntervieweeId] [bigint] NOT NULL,
	[SetQuestionId] [bigint] NOT NULL,
	[subjectiveAnswer] [nvarchar](max) NULL,
	[ObjectiveAnswer] [nvarchar](1000) NULL,
	[AnsweredBy] [bigint] NULL,
	[AuditTs] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL,
	[IsChecked] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AnswerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CheckedBy]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CheckedBy](
	[CheckedById] [int] NOT NULL,
	[Title] [nvarchar](500) NULL,
	[Deleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CheckedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentCode] [nvarchar](50) NULL,
	[DepartmentName] [nvarchar](200) NULL,
	[Deleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Exams]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exams](
	[IntervieweeId] [bigint] NOT NULL,
	[SetQuestion_id] [bigint] NOT NULL,
	[subjectiveAnswer] [nvarchar](max) NULL,
	[ObjectiveAnswer] [nvarchar](1000) NULL,
	[MarksObtained] [decimal](18, 0) NULL,
	[CheckedBy] [bigint] NULL,
	[AuditTs] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExamSets]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExamSets](
	[ExamSetId] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](1000) NOT NULL,
	[JobTitleId] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[TotalMark] [decimal](5, 2) NULL,
	[CreatedBy] [bigint] NOT NULL,
	[AuditTs] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL,
	[ExamDuration] [int] NULL,
 CONSTRAINT [PK__Question__D5617EEC86876B22] PRIMARY KEY CLUSTERED 
(
	[ExamSetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Interviewees]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Interviewees](
	[IntervieweeId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmailAddress] [nvarchar](100) NOT NULL,
	[InterviewSessionId] [bigint] NOT NULL,
	[FirstName] [nvarchar](200) NOT NULL,
	[MiddleName] [nvarchar](200) NULL,
	[LastName] [nvarchar](200) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[ContactNumber] [nvarchar](200) NOT NULL,
	[Attachments] [nvarchar](1000) NULL,
	[Deleted] [bit] NOT NULL,
	[JobTitleId] [bigint] NULL,
	[AttendedExam] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IntervieweeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InterviewSessions]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InterviewSessions](
	[InterviewSessionId] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](500) NULL,
	[SessionStartDate] [datetime] NULL,
	[SessionEndDate] [datetime] NULL,
	[CreatedBy] [bigint] NOT NULL,
	[AuditTs] [datetime] NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK__Intervie__FE0D335C74F479ED] PRIMARY KEY CLUSTERED 
(
	[InterviewSessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobTitles]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobTitles](
	[JobTitleId] [int] IDENTITY(1,1) NOT NULL,
	[JobTitle] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Deleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[JobTitleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ObjectiveQuestionOptions]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ObjectiveQuestionOptions](
	[ObjectiveQuestionOptionId] [bigint] IDENTITY(1,1) NOT NULL,
	[QuestionId] [bigint] NOT NULL,
	[AnswerOption] [nvarchar](max) NULL,
	[Attachment] [nvarchar](1000) NULL,
	[IsAnswer] [bit] NULL,
	[Deleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ObjectiveQuestionOptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionBank]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionBank](
	[QuestionId] [bigint] IDENTITY(1,1) NOT NULL,
	[QuestionTypeId] [int] NOT NULL,
	[QuestionCategoryId] [int] NOT NULL,
	[Question] [nvarchar](max) NULL,
	[Attachment] [nvarchar](1000) NULL,
	[QuestionComplexityId] [int] NOT NULL,
	[Marks] [decimal](5, 2) NULL,
	[PreparedBy] [bigint] NOT NULL,
	[AuditTs] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK__Question__0DC06FAC89B5E4A7] PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionCategories]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionCategories](
	[QuestionCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryCode] [nvarchar](50) NOT NULL,
	[CategoryName] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Deleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[QuestionCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionComplexities]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionComplexities](
	[QuestionComplexityId] [int] IDENTITY(1,1) NOT NULL,
	[ComplexityCode] [nvarchar](50) NOT NULL,
	[ComplexityTitle] [nvarchar](100) NOT NULL,
	[Marks] [decimal](5, 2) NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK__Question__42CA3BC7910A447D] PRIMARY KEY CLUSTERED 
(
	[QuestionComplexityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionTypes]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionTypes](
	[QuestionTypeId] [int] IDENTITY(1,1) NOT NULL,
	[TypeCode] [nvarchar](50) NOT NULL,
	[TypeName] [nvarchar](200) NOT NULL,
	[Deleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[QuestionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Results]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Results](
	[ResultId] [bigint] IDENTITY(1,1) NOT NULL,
	[AnswerId] [bigint] NOT NULL,
	[MarksObtained] [decimal](18, 0) NOT NULL,
	[Remarks] [nvarchar](500) NULL,
	[CheckedBy] [int] NOT NULL,
	[ExaminerId] [bigint] NULL,
	[AuditTs] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK__Results__97690208593FBB70] PRIMARY KEY CLUSTERED 
(
	[ResultId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [nvarchar](100) NOT NULL,
	[Title] [nvarchar](500) NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK__Roles__8AFACE1AAB12E6EC] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SessionwiseJobs]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SessionwiseJobs](
	[SessionwiseJobId] [bigint] IDENTITY(1,1) NOT NULL,
	[InterviewSessionId] [bigint] NOT NULL,
	[JobTitleId] [int] NOT NULL,
	[ExamSetId] [bigint] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[AuditTs] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SessionwiseJobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SetQuestions]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SetQuestions](
	[SetQuestionId] [bigint] IDENTITY(1,1) NOT NULL,
	[ExamSetId] [bigint] NOT NULL,
	[QuestionId] [bigint] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[AuditTs] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK__SetQuest__2A0CCFC4A7E14738] PRIMARY KEY CLUSTERED 
(
	[SetQuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](100) NULL,
	[EmailAddress] [nvarchar](100) NULL,
	[Password] [nvarchar](100) NOT NULL,
	[FirstName] [nvarchar](200) NOT NULL,
	[MiddleName] [nvarchar](200) NULL,
	[LastName] [nvarchar](200) NOT NULL,
	[DepartmentId] [int] NULL,
	[Deleted] [bit] NOT NULL,
	[PasswordChanged] [bit] NOT NULL,
 CONSTRAINT [PK__Users__1788CC4C8AC2126B] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[QuestionBankView]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[QuestionBankView]
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
GO
/****** Object:  View [dbo].[InterviewQuestions]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[InterviewQuestions]
AS
SELECT 
	 dbo.QuestionBankView.QuestionId,
	 dbo.QuestionBankView.Question,
	 dbo.QuestionBankView.QuestionTypeId,
	 dbo.QuestionBankView.Attachment,
	 dbo.QuestionBankView.Marks,
	 dbo.QuestionBankView.QuestionCategoryCode,
	 dbo.QuestionBankView.QuestionCategoryId,
	 dbo.QuestionBankView.QuestionCategoryName,
	 dbo.QuestionBankView.QuestionComplexityCode,
	 dbo.QuestionBankView.QuestionComplexityId,
	 dbo.QuestionBankView.QuestionComplexityName,
	 dbo.QuestionBankView.QuestionTypeCode,
	 dbo.QuestionBankView.QuestionTypeName,
	 dbo.Interviewees.IntervieweeId,
	 dbo.SetQuestions.SetQuestionId
 FROM dbo.Interviewees
INNER JOIN dbo.SessionWiseJobs
ON Interviewees.InterviewSessionId = dbo.SessionwiseJobs.InterviewSessionId
AND Interviewees.JobTitleId = dbo.SessionwiseJobs.JobTitleId
INNER JOIN dbo.SetQuestions
ON dbo.SetQuestions.ExamSetId = dbo.SessionwiseJobs.ExamSetId
INNER JOIN dbo.QuestionBankView
ON dbo.QuestionBankView.QuestionId = dbo.SetQuestions.QuestionId
WHERE dbo.Interviewees.deleted = 0
AND SessionWiseJobs.Deleted = 0
AND dbo.SetQuestions.Deleted = 0;

GO
/****** Object:  View [dbo].[InterviewQuestionsToCheckAnswersView]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[InterviewQuestionsToCheckAnswersView]
AS
SELECT
	InterviewQuestions.QuestionId ,
	InterviewQuestions.IntervieweeId,
	InterviewQuestions.Attachment,
	InterviewQuestions.Marks,
	InterviewQuestions.Question,
	InterviewQuestions.SetQuestionId,
	InterviewQuestions.QuestionTypeId,
	AnswersByInterviewees.AnswerId,
	AnswersByInterviewees.subjectiveAnswer,
	Results.ResultId,
	Results.MarksObtained,
	Results.Remarks
FROM InterviewQuestions
INNER JOIN AnswersByInterviewees 
ON InterviewQuestions.SetQuestionId = AnswersByInterviewees.SetQuestionId
AND InterviewQuestions.IntervieweeId = AnswersByInterviewees.IntervieweeId
LEFT JOIN Results 
ON Results.AnswerId = AnswersByInterviewees.AnswerId
GO
/****** Object:  View [dbo].[ObjectiveOptionsWithAnswersByIntervieweesView]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ObjectiveOptionsWithAnswersByIntervieweesView]
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

GO
/****** Object:  UserDefinedFunction [dbo].[ConvertDelimitedListIntoTable]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ConvertDelimitedListIntoTable] (
     @objectiveAnswers NVARCHAR(MAX) )
RETURNS TABLE 
AS 
    RETURN 
	(SELECT LTRIM(RTRIM(Split.a.value('.', 'VARCHAR(100)'))) 'ObjectiveQuestionId',CAST(1 AS bit) as Status
		FROM  
		(     
			 SELECT CAST ('<M>' + REPLACE(@objectiveAnswers,',', '</M><M>') + '</M>' AS XML) AS Data            
		) AS A 
	CROSS APPLY Data.nodes ('/M') AS Split(a))














GO
/****** Object:  View [dbo].[ActiveInterviewSessionView]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ActiveInterviewSessionView]
AS
SELECT 
	InterviewSessions.InterviewSessionId,
	InterviewSessions.Title,
	InterviewSessions.SessionStartDate,
	InterviewSessions.SessionEndDate
FROM  InterviewSessions
WHERE InterviewSessions.SessionStartDate <= GetDate()
AND InterviewSessions.SessionEndDate >= GetDate()
AND InterviewSessions.Deleted = 0;


GO
/****** Object:  View [dbo].[IntervieweeView]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[IntervieweeView]
AS
SELECT  
	interviewees.IntervieweeId,
	interviewees.EmailAddress,
	interviewees.InterviewSessionId,
	interviewees.FirstName,
	interviewees.MiddleName,
	interviewees.LastName,
	LTRIM(interviewees.FirstName)+' '+ LTRIM(LTRIM(interviewees.MiddleName)+' ' + LTRIM(interviewees.LastName)) AS FullName,
	interviewees.Address,
	interviewees.ContactNumber,
	interviewees.Attachments,
	Interviewees.AttendedExam,
	interviewees.JobTitleId,
	JobTitles.JobTitle
FROM interviewees
INNER JOIN JobTitles
ON Interviewees.JobTitleId = JobTitles.JobTitleId
WHERE interviewees.Deleted = 0
AND JobTitles.Deleted =0
GO
/****** Object:  View [dbo].[InterviewResultSummaryView]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[InterviewResultSummaryView]
AS
SELECT 
	Interviewees.IntervieweeId,
	Interviewees.InterviewSessionId,
	Interviewees.EmailAddress,
	LTrim(LTrim(Interviewees.FirstName) +' '+ LTRIM(LTRIM(Interviewees.MiddleName)+ ' '+Interviewees.LastName)) AS IntervieweeName,
	JobTitles.JobTitle,
	JobTitles.JobTitleId,
	Interviewees.Address,
	Interviewees.ContactNumber,
	SUM(ISNULL(Results.MarksObtained,0)) AS MarksObtained
FROM Interviewees
LEFT JOIN AnswersByInterviewees
ON AnswersByInterviewees.IntervieweeId = Interviewees.IntervieweeId
INNER JOIN Results
ON Results.AnswerId = AnswersByInterviewees.AnswerId
INNER JOIN JobTitles
ON JobTitles.JobTitleId = Interviewees.JobTitleId
WHERE Interviewees.Deleted = 0
AND AnswersByInterviewees.Deleted = 0
AND Results.Deleted = 0
GROUP BY 
	Interviewees.IntervieweeId,
	Interviewees.InterviewSessionId,
	Interviewees.EmailAddress,
	Interviewees.FirstName,
	Interviewees.MiddleName,
	Interviewees.LastName,
	JobTitles.JobTitle,
	Interviewees.Address,
	Interviewees.ContactNumber,
	JobTitles.JobTitleId

	

	

	
GO
/****** Object:  View [dbo].[InterviewSessionCandidate_view]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[InterviewSessionCandidate_view]
AS
SELECT 
	Interviewees.IntervieweeId,
	Interviewees.EmailAddress,
	Interviewees.InterviewSessionId,
	Interviewees.FirstName,
	Interviewees.MiddleName,
	Interviewees.LastName,
	LTRIM(Interviewees.FirstName) +' '+ LTRIM(LTRIM(Interviewees.MiddleName) +' '+ LTRIM(Interviewees.LastName)) AS FullName,
	Interviewees.Address,
	Interviewees.ContactNumber,
	Interviewees.Attachments,
	Interviewees.AttendedExam,
	InterviewSessions.Title AS SessionTitle,
	JobTitles.JobTitle,
	JobTitles.JobTitleId
FROM interviewees
INNER JOIN InterviewSessions
ON interviewees.InterviewSessionId = InterviewSessions.InterviewSessionId
INNER JOIN JobTitles
ON JobTitles.JobTitleId = interviewees.JobTitleId
WHERE InterviewSessions.SessionStartDate <= GetDate()
AND InterviewSessions.SessionEndDate >= GetDate()
AND InterviewSessions.Deleted = 0
AND interviewees.Deleted = 0;





GO
/****** Object:  View [dbo].[InterviewSessionHistoryView]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[InterviewSessionHistoryView]
AS
SELECT 
	InterviewSessions.InterviewSessionId,
	InterviewSessions.Title,
	InterviewSessions.SessionStartDate,
	InterviewSessions.SessionEndDate
FROM  InterviewSessions
WHERE NOT (InterviewSessions.SessionStartDate <= GetDate()
AND InterviewSessions.SessionEndDate >= GetDate())
AND InterviewSessions.Deleted = 0;



GO
/****** Object:  View [dbo].[QuestionsForSetView]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[QuestionsForSetView]
As
SELECT 
	SetQuestions.SetQuestionId,
	SetQuestions.ExamSetId,
	QuestionBank.QuestionId,
	QuestionBank.QuestionTypeId,
	QuestionBank.Marks,
	QuestionTypes.TypeName AS QuestionTypeName,
	QuestionTypes.TypeCode AS QuestionTypeCode,
	QuestionBank.QuestionCategoryId,
	QuestionCategories.CategoryCode AS QuestionCategoryCode,
	QuestionCategories.CategoryName AS QuestionCategoryName,
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
INNER JOIN QuestionComplexities
ON QuestionComplexities.QuestionComplexityId = QuestionBank.QuestionComplexityId
WHERE SetQuestions.Deleted = 0









GO
/****** Object:  View [dbo].[SessionJobView]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[SessionJobView]
AS
SELECT 
	SessionwiseJobs.SessionwiseJobId,
	SessionwiseJobs.JobTitleId,
	SessionwiseJobs.InterviewSessionId,
	ExamSets.ExamSetId,
	ExamSets.Title AS ExamSetTitle,
	JobTitles.JobTitle
FROM SessionwiseJobs
INNER JOIN ExamSets
ON SessionwiseJobs.ExamSetId = ExamSets.ExamSetId
INNER JOIN JobTitles
ON SessionwiseJobs.JobTitleId = JobTitles.JobTitleId
WHERE SessionwiseJobs.Deleted = 0;
GO
ALTER TABLE [dbo].[AnswersByInterviewees] ADD  DEFAULT (getdate()) FOR [AuditTs]
GO
ALTER TABLE [dbo].[AnswersByInterviewees] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[AnswersByInterviewees] ADD  CONSTRAINT [DF_AnswersByInterviewees_IsChecked]  DEFAULT ((0)) FOR [IsChecked]
GO
ALTER TABLE [dbo].[CheckedBy] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Departments] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Exams] ADD  DEFAULT (getdate()) FOR [AuditTs]
GO
ALTER TABLE [dbo].[Exams] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[ExamSets] ADD  CONSTRAINT [DF__QuestionS__Audit__236943A5]  DEFAULT (getdate()) FOR [AuditTs]
GO
ALTER TABLE [dbo].[ExamSets] ADD  CONSTRAINT [DF__QuestionS__Delet__245D67DE]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Interviewees] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Interviewees] ADD  CONSTRAINT [DF_Interviewees_AttendedExam]  DEFAULT ((0)) FOR [AttendedExam]
GO
ALTER TABLE [dbo].[InterviewSessions] ADD  CONSTRAINT [DF__Interview__Audit__2EDAF651]  DEFAULT (getdate()) FOR [AuditTs]
GO
ALTER TABLE [dbo].[InterviewSessions] ADD  CONSTRAINT [DF__Interview__Delet__2FCF1A8A]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[JobTitles] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[ObjectiveQuestionOptions] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[QuestionBank] ADD  CONSTRAINT [DF__Questions__Audit__19DFD96B]  DEFAULT (getdate()) FOR [AuditTs]
GO
ALTER TABLE [dbo].[QuestionBank] ADD  CONSTRAINT [DF__Questions__Delet__1AD3FDA4]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[QuestionCategories] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[QuestionComplexities] ADD  CONSTRAINT [DF__QuestionC__Delet__123EB7A3]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[QuestionTypes] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Results] ADD  CONSTRAINT [DF__Results__AuditTs__18B6AB08]  DEFAULT (getdate()) FOR [AuditTs]
GO
ALTER TABLE [dbo].[Results] ADD  CONSTRAINT [DF__Results__Deleted__19AACF41]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[SessionwiseJobs] ADD  DEFAULT (getdate()) FOR [AuditTs]
GO
ALTER TABLE [dbo].[SessionwiseJobs] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[SetQuestions] ADD  CONSTRAINT [DF__SetQuesti__Audit__29221CFB]  DEFAULT (getdate()) FOR [AuditTs]
GO
ALTER TABLE [dbo].[SetQuestions] ADD  CONSTRAINT [DF__SetQuesti__Delet__2A164134]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__Deleted__06CD04F7]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_PasswordChanged]  DEFAULT ((0)) FOR [PasswordChanged]
GO
ALTER TABLE [dbo].[AnswersByInterviewees]  WITH CHECK ADD FOREIGN KEY([AnsweredBy])
REFERENCES [dbo].[Interviewees] ([IntervieweeId])
GO
ALTER TABLE [dbo].[AnswersByInterviewees]  WITH CHECK ADD FOREIGN KEY([IntervieweeId])
REFERENCES [dbo].[Interviewees] ([IntervieweeId])
GO
ALTER TABLE [dbo].[AnswersByInterviewees]  WITH CHECK ADD FOREIGN KEY([SetQuestionId])
REFERENCES [dbo].[SetQuestions] ([SetQuestionId])
GO
ALTER TABLE [dbo].[Exams]  WITH CHECK ADD  CONSTRAINT [FK__Exams__CheckedBy__37703C52] FOREIGN KEY([CheckedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Exams] CHECK CONSTRAINT [FK__Exams__CheckedBy__37703C52]
GO
ALTER TABLE [dbo].[Exams]  WITH CHECK ADD FOREIGN KEY([IntervieweeId])
REFERENCES [dbo].[Interviewees] ([IntervieweeId])
GO
ALTER TABLE [dbo].[Exams]  WITH CHECK ADD  CONSTRAINT [FK__Exams__SetQuesti__367C1819] FOREIGN KEY([SetQuestion_id])
REFERENCES [dbo].[SetQuestions] ([SetQuestionId])
GO
ALTER TABLE [dbo].[Exams] CHECK CONSTRAINT [FK__Exams__SetQuesti__367C1819]
GO
ALTER TABLE [dbo].[ExamSets]  WITH CHECK ADD  CONSTRAINT [FK__QuestionS__Creat__22751F6C] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[ExamSets] CHECK CONSTRAINT [FK__QuestionS__Creat__22751F6C]
GO
ALTER TABLE [dbo].[ExamSets]  WITH CHECK ADD  CONSTRAINT [FK__QuestionS__JobTi__2180FB33] FOREIGN KEY([JobTitleId])
REFERENCES [dbo].[JobTitles] ([JobTitleId])
GO
ALTER TABLE [dbo].[ExamSets] CHECK CONSTRAINT [FK__QuestionS__JobTi__2180FB33]
GO
ALTER TABLE [dbo].[Interviewees]  WITH CHECK ADD  CONSTRAINT [FK__Interview__Inter__32AB8735] FOREIGN KEY([InterviewSessionId])
REFERENCES [dbo].[InterviewSessions] ([InterviewSessionId])
GO
ALTER TABLE [dbo].[Interviewees] CHECK CONSTRAINT [FK__Interview__Inter__32AB8735]
GO
ALTER TABLE [dbo].[InterviewSessions]  WITH CHECK ADD  CONSTRAINT [FK__Interview__Creat__2DE6D218] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[InterviewSessions] CHECK CONSTRAINT [FK__Interview__Creat__2DE6D218]
GO
ALTER TABLE [dbo].[ObjectiveQuestionOptions]  WITH CHECK ADD  CONSTRAINT [FK__Objective__Quest__1DB06A4F] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[QuestionBank] ([QuestionId])
GO
ALTER TABLE [dbo].[ObjectiveQuestionOptions] CHECK CONSTRAINT [FK__Objective__Quest__1DB06A4F]
GO
ALTER TABLE [dbo].[QuestionBank]  WITH CHECK ADD  CONSTRAINT [FK__Questions__Prepa__18EBB532] FOREIGN KEY([PreparedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[QuestionBank] CHECK CONSTRAINT [FK__Questions__Prepa__18EBB532]
GO
ALTER TABLE [dbo].[QuestionBank]  WITH CHECK ADD  CONSTRAINT [FK__Questions__Quest__151B244E] FOREIGN KEY([QuestionTypeId])
REFERENCES [dbo].[QuestionTypes] ([QuestionTypeId])
GO
ALTER TABLE [dbo].[QuestionBank] CHECK CONSTRAINT [FK__Questions__Quest__151B244E]
GO
ALTER TABLE [dbo].[QuestionBank]  WITH CHECK ADD  CONSTRAINT [FK__Questions__Quest__160F4887] FOREIGN KEY([QuestionCategoryId])
REFERENCES [dbo].[QuestionCategories] ([QuestionCategoryId])
GO
ALTER TABLE [dbo].[QuestionBank] CHECK CONSTRAINT [FK__Questions__Quest__160F4887]
GO
ALTER TABLE [dbo].[QuestionBank]  WITH CHECK ADD  CONSTRAINT [FK__Questions__Quest__17F790F9] FOREIGN KEY([QuestionComplexityId])
REFERENCES [dbo].[QuestionComplexities] ([QuestionComplexityId])
GO
ALTER TABLE [dbo].[QuestionBank] CHECK CONSTRAINT [FK__Questions__Quest__17F790F9]
GO
ALTER TABLE [dbo].[Results]  WITH CHECK ADD  CONSTRAINT [FK__Results__AnswerI__15DA3E5D] FOREIGN KEY([AnswerId])
REFERENCES [dbo].[AnswersByInterviewees] ([AnswerId])
GO
ALTER TABLE [dbo].[Results] CHECK CONSTRAINT [FK__Results__AnswerI__15DA3E5D]
GO
ALTER TABLE [dbo].[Results]  WITH CHECK ADD  CONSTRAINT [FK__Results__Checked__16CE6296] FOREIGN KEY([CheckedBy])
REFERENCES [dbo].[CheckedBy] ([CheckedById])
GO
ALTER TABLE [dbo].[Results] CHECK CONSTRAINT [FK__Results__Checked__16CE6296]
GO
ALTER TABLE [dbo].[Results]  WITH CHECK ADD  CONSTRAINT [FK__Results__Examine__17C286CF] FOREIGN KEY([ExaminerId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Results] CHECK CONSTRAINT [FK__Results__Examine__17C286CF]
GO
ALTER TABLE [dbo].[SessionwiseJobs]  WITH CHECK ADD  CONSTRAINT [FK__Sessionwi__Creat__662B2B3B] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[SessionwiseJobs] CHECK CONSTRAINT [FK__Sessionwi__Creat__662B2B3B]
GO
ALTER TABLE [dbo].[SessionwiseJobs]  WITH CHECK ADD  CONSTRAINT [FK__Sessionwi__Inter__634EBE90] FOREIGN KEY([InterviewSessionId])
REFERENCES [dbo].[InterviewSessions] ([InterviewSessionId])
GO
ALTER TABLE [dbo].[SessionwiseJobs] CHECK CONSTRAINT [FK__Sessionwi__Inter__634EBE90]
GO
ALTER TABLE [dbo].[SessionwiseJobs]  WITH CHECK ADD FOREIGN KEY([JobTitleId])
REFERENCES [dbo].[JobTitles] ([JobTitleId])
GO
ALTER TABLE [dbo].[SessionwiseJobs]  WITH CHECK ADD  CONSTRAINT [FK__Sessionwi__Quest__65370702] FOREIGN KEY([ExamSetId])
REFERENCES [dbo].[ExamSets] ([ExamSetId])
GO
ALTER TABLE [dbo].[SessionwiseJobs] CHECK CONSTRAINT [FK__Sessionwi__Quest__65370702]
GO
ALTER TABLE [dbo].[SetQuestions]  WITH CHECK ADD  CONSTRAINT [FK__SetQuesti__Creat__282DF8C2] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[SetQuestions] CHECK CONSTRAINT [FK__SetQuesti__Creat__282DF8C2]
GO
ALTER TABLE [dbo].[SetQuestions]  WITH CHECK ADD  CONSTRAINT [FK__SetQuesti__Quest__2739D489] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[QuestionBank] ([QuestionId])
GO
ALTER TABLE [dbo].[SetQuestions] CHECK CONSTRAINT [FK__SetQuesti__Quest__2739D489]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK__Users__Departmen__05D8E0BE] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([DepartmentId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK__Users__Departmen__05D8E0BE]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK__Users__RoleId__04E4BC85] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK__Users__RoleId__04E4BC85]
GO
/****** Object:  StoredProcedure [dbo].[ObjectiveIptionsToCheckAnswersProcedure]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[ObjectiveIptionsToCheckAnswersProcedure]
@intervieweeId bigint
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
	WHERE InterviewQuestions.IntervieweeId = @IntervieweeId
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
SELECT * FROM dbo.InterviewQuestionsToCheckAnswersView
SELECT * FROM #tmpObjectiveOptionWithAnswerByInterviewee;

DROP TABLE #tmpObjectiveOptionWithAnswerByInterviewee;

END



GO
/****** Object:  StoredProcedure [dbo].[sp_check_objective_answers]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_check_objective_answers]
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
				AND ObjectiveQuestionOptions.IsAnswer = 1
				AND ObjectiveQuestionOptions.Deleted = 0;

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
GO
/****** Object:  StoredProcedure [dbo].[sp_validateInterviewee]    Script Date: 10/25/2017 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_validateInterviewee] 
	@emailaddress nvarchar(100),
	@contactnumber nvarchar(200),
	@Result bit = 0  Output
AS
BEGIN
	IF EXISTS (SELECT * FROM dbo.InterviewSessionCandidate_view 
	WHERE EmailAddress = @emailaddress
    AND ContactNumber = @ContactNumber
	--AND AttendedExam = 0
	)
	BEGIN
	print 'valid'
	set @Result = (SELECT CAST(1 AS bit) as Result)
	END
	ELSE
	begin
	set @Result =(SELECT CAST(0 AS bit) as Result)
	end
END
GO
USE [master]
GO
ALTER DATABASE [VExam] SET  READ_WRITE 
GO
