ALTER TABLE dbo.Discounts
    ADD CONSTRAINT FK_CarTypes_Discounts
    FOREIGN KEY ([CarTypeId])     
    REFERENCES dbo.CarTypes (Id) ON UPDATE CASCADE ON DELETE CASCADE