CREATE TABLE [luxoft].[RefMachineParts]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL
)

GO

CREATE UNIQUE INDEX [UIX_RefMachineParts_Name] ON [luxoft].[RefMachineParts] ([Name])
