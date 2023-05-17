ALTER TABLE dbo.Discounts
    ADD CONSTRAINT FK_ServiceDatas_Discounts
    FOREIGN KEY ([ServiceDataId])     
    REFERENCES dbo.ServiceDatas (Id) ON UPDATE CASCADE ON DELETE CASCADE