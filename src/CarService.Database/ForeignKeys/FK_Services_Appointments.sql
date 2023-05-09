ALTER TABLE dbo.Appointments
    ADD CONSTRAINT FK_Services_Appointments
    FOREIGN KEY ([ServiceId])
    REFERENCES dbo.Services (Id) ON UPDATE CASCADE ON DELETE CASCADE