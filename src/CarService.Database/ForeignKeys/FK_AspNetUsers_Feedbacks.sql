    ALTER TABLE dbo.Feedbacks
    ADD CONSTRAINT FK_AspNetUsers_Feedbacks
    FOREIGN KEY ([UserId])     
    REFERENCES dbo.AspNetUsers (Id) ON UPDATE CASCADE ON DELETE CASCADE