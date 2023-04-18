CREATE TABLE [dbo].[ServicesAppointments]
(
	[Id] INT identity PRIMARY KEY, 
    [ServiceId] INT NOT NULL, 
    [AppointmentId] INT NOT NULL, 
)
