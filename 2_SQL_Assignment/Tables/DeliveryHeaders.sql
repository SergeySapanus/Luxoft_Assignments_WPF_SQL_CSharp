CREATE TABLE [luxoft].[DeliveryHeaders]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdRegion] UNIQUEIDENTIFIER NOT NULL, 
    [IdSupplier] UNIQUEIDENTIFIER NOT NULL, 
    [IdCurrency] UNIQUEIDENTIFIER NOT NULL, 
    [Date] DATETIME2 NOT NULL, 
    CONSTRAINT [FK_DeliveryHeaders_RefRegions] FOREIGN KEY ([IdRegion]) REFERENCES [luxoft].[RefRegions]([Id]),
    CONSTRAINT [FK_DeliveryHeaders_RefSuppliers] FOREIGN KEY ([IdSupplier]) REFERENCES [luxoft].[RefSuppliers]([Id]),
    CONSTRAINT [FK_DeliveryHeaders_RefCurrencies] FOREIGN KEY ([IdCurrency]) REFERENCES [luxoft].[RefCurrencies]([Id])
)
