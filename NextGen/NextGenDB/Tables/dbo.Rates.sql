﻿
CREATE TABLE [dbo].[Rates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Rate] [int] NOT NULL,
	[From] [int] NOT NULL,
	[To] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL DEFAULT GETDATE(),
	CONSTRAINT [PK_dbo.Rates] PRIMARY KEY ([Id])
)
