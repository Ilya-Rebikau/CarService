ALTER TABLE dbo.Discounts
    ADD CONSTRAINT FK_CarBrands_Discounts
    FOREIGN KEY ([CarBrandId])     
    REFERENCES dbo.CarBrands (Id) ON UPDATE CASCADE ON DELETE CASCADE