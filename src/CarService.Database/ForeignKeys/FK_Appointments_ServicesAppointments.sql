ALTER TABLE dbo.ServicesAppointments
    ADD CONSTRAINT FK_Appointments_ServicesAppointments
    FOREIGN KEY ([AppointmentId])     
    REFERENCES dbo.Appointments (Id) ON UPDATE CASCADE ON DELETE CASCADE