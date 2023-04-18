CREATE TABLE [dbo].[Discounts]
(
	[Id] INT identity PRIMARY KEY, 
    [Percent] SMALLINT NOT NULL, 
    [DateStart] TEXT NOT NULL, 
    [DateEnd] TEXT NOT NULL, 
    [ServiceId] INT NOT NULL
)
