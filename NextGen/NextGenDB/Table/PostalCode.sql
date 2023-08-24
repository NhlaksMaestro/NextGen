CREATE TABLE [dbo].[PostalCodes](
	[PostalCode] [int] NOT NULL,
	[TaxCalculationType] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL DEFAULT GETDATE(),
	[ModifiedDate] [datetime] NOT NULL DEFAULT GETDATE(),
	CONSTRAINT [PK_dbo.PostalCodes] PRIMARY KEY ([PostalCode])
)