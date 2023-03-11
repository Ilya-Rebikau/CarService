CREATE TABLE [dbo].[Discounts]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Percent] SMALLINT NOT NULL, 
    [TimeStart] TEXT NOT NULL, 
    [TimeEnd] TEXT NOT NULL, 
    [ServiceId] INT NOT NULL
)
