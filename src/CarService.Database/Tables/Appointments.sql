CREATE TABLE [dbo].[Appointments]
(
	[Id] INT identity PRIMARY KEY, 
    [CarMileage] FLOAT NOT NULL, 
    [Date] SMALLDATETIME NOT NULL, 
    [Message] NVARCHAR(MAX) NULL, 
    [UserId] NVARCHAR(450) NOT NULL
)
