    ALTER TABLE dbo.PersonalDiscounts
    ADD CONSTRAINT FK_AspNetUsers_PersonalDiscounts
    FOREIGN KEY ([UserId])     
    REFERENCES dbo.AspNetUsers (Id) ON UPDATE CASCADE ON DELETE CASCADE