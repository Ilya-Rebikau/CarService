CREATE TABLE [dbo].[Services]
(
	[Id] INT identity PRIMARY KEY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Price] DECIMAL(18, 2) NOT NULL, 
    [TimeSpent] TEXT NOT NULL
)
