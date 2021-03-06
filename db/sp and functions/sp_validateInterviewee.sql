USE [VExam]
GO
/****** Object:  StoredProcedure [dbo].[sp_validateInterviewee]    Script Date: 8/11/2017 2:02:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_validateInterviewee] 
	@emailaddress nvarchar(100),
	@contactnumber nvarchar(200),
	@Result bit = 0  Output
AS
BEGIN
	IF EXISTS (SELECT * FROM dbo.InterviewSessionCandidate_view 
	WHERE EmailAddress = @emailaddress
    AND ContactNumber = @ContactNumber
	AND AttendedExam = 0)
	BEGIN
	print 'valid'
	set @Result = (SELECT CAST(1 AS bit) as Result)
	END
	ELSE
	begin
	set @Result =(SELECT CAST(0 AS bit) as Result)
	end
END
