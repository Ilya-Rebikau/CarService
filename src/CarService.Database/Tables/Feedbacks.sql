CREATE TABLE [dbo].[Feedbacks]
(
	[Id] INT identity PRIMARY KEY, 
    [Message] NVARCHAR(MAX) NOT NULL, 
    [UserId] NVARCHAR(450) NOT NULL, 
    [DateTime] DATETIME NOT NULL, 
)
