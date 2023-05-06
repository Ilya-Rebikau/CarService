﻿CREATE TABLE [dbo].[Discounts]
(
	[Id] INT identity PRIMARY KEY, 
    [Percent] SMALLINT NOT NULL, 
    [DateStart] DATETIME NOT NULL, 
    [DateEnd] DATETIME NOT NULL, 
    [ServiceDataId] INT NULL, 
    [CarBrandId] INT NULL, 
    [CarTypeId] INT NULL
)
