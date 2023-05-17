    ALTER TABLE dbo.Promocodes
    ADD CONSTRAINT FK_AspNetUsers_Promocodes
    FOREIGN KEY ([UserId])     
    REFERENCES dbo.AspNetUsers (Id) ON UPDATE CASCADE ON DELETE CASCADE