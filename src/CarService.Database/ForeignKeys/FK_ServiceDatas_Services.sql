ALTER TABLE dbo.Services
    ADD CONSTRAINT FK_ServiceDatas_Services
    FOREIGN KEY ([ServiceDataId])     
    REFERENCES dbo.ServiceDatas (Id) ON UPDATE CASCADE ON DELETE CASCADE