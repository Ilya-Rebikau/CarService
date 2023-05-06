CREATE TABLE [dbo].[Discounts]
(
	[Id] INT identity PRIMARY KEY, 
    [Percent] INT NOT NULL, 
    [DateStart] DATE NOT NULL, 
    [DateEnd] DATE NOT NULL, 
    [ServiceDataId] INT NULL, 
    [CarBrandId] INT NULL, 
    [CarTypeId] INT NULL
)
