CREATE TABLE [dbo].[Services]
(
	[Id] INT identity PRIMARY KEY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Price] DECIMAL(18, 2) NOT NULL, 
    [MinutesSpent] INT NOT NULL, 
    [CarBrandId] INT NOT NULL, 
    [CarTypeId] INT NOT NULL, 
    [ImageData] VARBINARY(MAX) NULL
)
