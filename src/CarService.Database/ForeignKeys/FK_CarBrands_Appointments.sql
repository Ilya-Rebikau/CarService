ALTER TABLE dbo.Appointments
    ADD CONSTRAINT FK_CarBrands_Appointments
    FOREIGN KEY ([CarBrandId])     
    REFERENCES dbo.CarBrands (Id) ON UPDATE CASCADE ON DELETE CASCADE