ALTER TABLE dbo.PersonalDiscounts
    ADD CONSTRAINT FK_Services_PersonalDiscounts
    FOREIGN KEY ([ServiceId])     
    REFERENCES dbo.Services (Id) ON UPDATE CASCADE ON DELETE CASCADE