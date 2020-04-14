CREATE TABLE [luxoft].[RefSuppliers]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL
)

GO

CREATE UNIQUE INDEX [UIX_RefSuppliers_Name] ON [luxoft].[RefSuppliers] ([Name])
