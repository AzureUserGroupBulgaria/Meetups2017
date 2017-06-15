GO

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[EGN] [nvarchar](10) NOT NULL,
	[CreditCard] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

GO

INSERT INTO [dbo].[Users] ([FirstName] ,[LastName] ,[EGN] ,[CreditCard])
     VALUES ('Gilfy' ,'Sigurdsson' ,'8034128899' ,'4334-5444-3232-4545')
GO

INSERT INTO [dbo].[Users] ([FirstName] ,[LastName] ,[EGN] ,[CreditCard])
     VALUES ('Philippe' ,'Coutinho' ,'8202128899' ,'8834-5664-3232-4545')
GO

INSERT INTO [dbo].[Users] ([FirstName] ,[LastName] ,[EGN] ,[CreditCard])
     VALUES ('Danny' ,'Drinkwater' ,'9034128899' ,'5555-7844-3232-3331')
GO