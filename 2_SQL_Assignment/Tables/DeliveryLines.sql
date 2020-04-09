CREATE TABLE [luxoft].[DeliveryLines]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdDeliveryHeader] UNIQUEIDENTIFIER NOT NULL, 
    [Price] MONEY NOT NULL, 
    [Number] NUMERIC NOT NULL , 
    CONSTRAINT [FK_DeliveryLines_DeliveryHeaders] FOREIGN KEY ([IdDeliveryHeader]) REFERENCES [luxoft].[DeliveryHeaders]([Id])
)
