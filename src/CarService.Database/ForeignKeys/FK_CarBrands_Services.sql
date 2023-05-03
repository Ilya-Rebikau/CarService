ALTER TABLE dbo.Services
    ADD CONSTRAINT FK_CarBrands_Services
    FOREIGN KEY ([CarBrandId])     
    REFERENCES dbo.CarBrands (Id) ON UPDATE CASCADE ON DELETE CASCADE