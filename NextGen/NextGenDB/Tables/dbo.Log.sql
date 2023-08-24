CREATE TABLE [dbo].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Logged] [datetime] NOT NULL,
	[Level] [varchar](5) NOT NULL,
	[Message] [varchar](max) NOT NULL,
	[Properties] [varchar](max) NULL,
	[Exception] [varchar](max) NULL, 
    CONSTRAINT [PK_security.Log] PRIMARY KEY ([Id])
) 
GO