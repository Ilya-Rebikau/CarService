CREATE TABLE [dbo].[Promocodes]
(
	[Id] INT identity PRIMARY KEY, 
    [Percent] INT NOT NULL, 
    [DateEnd] DATETIME NOT NULL, 
    , 
    [UserId] NVARCHAR(450) NULL, 
    [Text] NVARCHAR(10) UNIQUE NOT NULL
)