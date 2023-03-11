CREATE TABLE [dbo].[ServicesAppointments]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ServiceId] INT NOT NULL, 
    [AppointmentId] INT NOT NULL, 
    [Message] NVARCHAR(MAX) NULL
)
