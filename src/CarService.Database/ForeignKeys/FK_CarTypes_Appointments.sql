ALTER TABLE dbo.Appointments
    ADD CONSTRAINT FK_CarTypes_Appointments
    FOREIGN KEY ([CarTypeId])     
    REFERENCES dbo.CarTypes (Id) ON UPDATE CASCADE ON DELETE CASCADE