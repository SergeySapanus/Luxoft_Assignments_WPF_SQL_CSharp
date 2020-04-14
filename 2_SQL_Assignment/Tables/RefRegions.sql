CREATE TABLE [luxoft].[RefRegions]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL
)

GO

CREATE UNIQUE INDEX [UIX_RefRegions_Name] ON [luxoft].[RefRegions] ([Name])
