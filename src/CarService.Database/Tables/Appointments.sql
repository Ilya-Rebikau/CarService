CREATE TABLE [dbo].[Appointments]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CarTypeId] INT NOT NULL, 
    [CarBrandId] INT NOT NULL, 
    [CarMileage] FLOAT NOT NULL, 
    [Date] SMALLDATETIME NOT NULL, 
    [Message] NVARCHAR(MAX) NULL
)
