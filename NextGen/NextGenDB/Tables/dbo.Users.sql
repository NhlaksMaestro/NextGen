CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[EarningPerMonth] [decimal] NOT NULL,
	[EarningPerYear] [decimal] NULL,
	[RateId] [int] NULL,
	[RatePercentage] [varchar](5) NULL,
	[PostalCodeId] [varchar](10) NOT NULL,
	[CreatedDate] [datetime] NOT NULL DEFAULT GETDATE(),
	CONSTRAINT [PK_dbo_User] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_User_Rates] FOREIGN KEY([RateId])
		REFERENCES [dbo].[Rates] ([Id]),
	CONSTRAINT [FK_User_PostalCode] FOREIGN KEY([PostalCodeId])
		REFERENCES [dbo].[PostalCodes] ([PostalCode])
)
GO