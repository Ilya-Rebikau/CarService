CREATE TABLE [dbo].[Discounts]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Percent] SMALLINT NOT NULL, 
    [DateStart] TEXT NOT NULL, 
    [DateEnd] TEXT NOT NULL, 
    [ServiceId] INT NOT NULL
)
