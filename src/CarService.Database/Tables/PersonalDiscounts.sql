CREATE TABLE [dbo].[PersonalDiscounts]
(
	[Id] INT identity PRIMARY KEY, 
    [Percent] SMALLINT NOT NULL, 
    [DateStart] DATETIME NOT NULL, 
    [DateEnd] DATETIME NOT NULL, 
    [ServiceId] INT NOT NULL, 
    [UserId] NVARCHAR(450) NOT NULL, 
)
