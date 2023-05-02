ALTER TABLE dbo.Discounts
    ADD CONSTRAINT FK_Services_Discounts
    FOREIGN KEY ([ServiceId])     
    REFERENCES dbo.Services (Id) ON UPDATE CASCADE ON DELETE CASCADE