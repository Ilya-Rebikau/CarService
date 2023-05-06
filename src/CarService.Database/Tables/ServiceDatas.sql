CREATE TABLE [dbo].[ServiceDatas]
(
	[Id] INT identity PRIMARY KEY, 
	[Name] NVARCHAR(100) NOT NULL, 
	[ImageData] VARBINARY(MAX) NULL
)
