CREATE TABLE [dbo].[Services]
(
	[Id] INT identity PRIMARY KEY, 
    [Price] DECIMAL(18, 2) NOT NULL, 
    [MinutesSpent] INT NOT NULL, 
    [CarBrandId] INT NOT NULL, 
    [CarTypeId] INT NOT NULL, 
    [ServiceDataId] INT NOT NULL
)
