﻿ALTER TABLE dbo.Services
    ADD CONSTRAINT FK_CarTypes_Services
    FOREIGN KEY ([CarTypeId])     
    REFERENCES dbo.CarTypes (Id) ON UPDATE CASCADE ON DELETE CASCADE