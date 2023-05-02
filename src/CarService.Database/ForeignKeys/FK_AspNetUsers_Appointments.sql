ALTER TABLE dbo.Appointments
    ADD CONSTRAINT FK_AspNetUsers_Appointments
    FOREIGN KEY ([UserId])     
    REFERENCES dbo.AspNetUsers (Id) ON UPDATE CASCADE ON DELETE CASCADE