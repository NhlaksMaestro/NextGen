CREATE TABLE [dbo].[PostalCodes] (
    [PostalCode]         VARCHAR (10) NOT NULL,
    [TaxCalculationType] VARCHAR (50) NOT NULL,
    [CreatedDate]        DATETIME     NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [PK_dbo.PostalCodes] PRIMARY KEY CLUSTERED ([PostalCode] ASC)
);
