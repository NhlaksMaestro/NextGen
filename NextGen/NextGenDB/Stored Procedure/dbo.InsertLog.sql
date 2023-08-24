CREATE PROCEDURE [dbo].[InsertLog] (@logged DATETIME, @level VARCHAR(5), @message VARCHAR(MAX), @properties VARCHAR(MAX), @exception VARCHAR(MAX)) 
AS
BEGIN
	INSERT INTO [dbo].[Log] ([Logged], [Level], [Message], [Properties], [Exception]) 
	VALUES (@logged, @level, @message, @properties,@exception);
END