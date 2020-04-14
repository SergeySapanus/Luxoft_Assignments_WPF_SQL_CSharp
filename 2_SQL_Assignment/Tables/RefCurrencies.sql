CREATE TABLE [luxoft].[RefCurrencies]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [ShortName] NVARCHAR(3) NOT NULL
)

GO

CREATE UNIQUE INDEX [UIX_RefCurrencies_ShortName] ON [luxoft].[RefCurrencies] ([ShortName])
