CREATE TABLE [dbo].[Services]
(
	[Id] INT identity PRIMARY KEY, 
    [Price] DECIMAL(18, 2) NOT NULL, 
    [MinutesSpent] INT NOT NULL, 
    [CarBrandId] INT NULL, 
    [CarTypeId] INT NULL, 
    [ServiceDataId] INT NOT NULL
)
