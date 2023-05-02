ALTER TABLE dbo.ServicesAppointments
    ADD CONSTRAINT FK_Services_ServicesAppointments
    FOREIGN KEY ([ServiceId])     
    REFERENCES dbo.Services (Id) ON UPDATE CASCADE ON DELETE CASCADE