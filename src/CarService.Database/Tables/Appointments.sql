CREATE TABLE [dbo].[Appointments]
(
	[Id] INT identity PRIMARY KEY, 
    [Date] DATETIME NOT NULL, 
    [Message] NVARCHAR(MAX) NULL, 
    [UserId] NVARCHAR(450) NOT NULL, 
    [WasFinished] BIT NOT NULL, 
    [ServiceId] INT NOT NULL
)
