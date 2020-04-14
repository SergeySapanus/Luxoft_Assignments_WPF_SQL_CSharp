CREATE TABLE [luxoft].[DeliveryLines]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdDeliveryHeader] UNIQUEIDENTIFIER NOT NULL, 
    [IdMachinePart] UNIQUEIDENTIFIER NOT NULL, 
    [Number] NUMERIC NOT NULL , 
    [Price] MONEY NOT NULL, 
    CONSTRAINT [FK_DeliveryLines_DeliveryHeaders] FOREIGN KEY ([IdDeliveryHeader]) REFERENCES [luxoft].[DeliveryHeaders]([Id]), 
    CONSTRAINT [FK_DeliveryLines_RefMachineParts] FOREIGN KEY ([IdMachinePart]) REFERENCES [luxoft].[RefMachineParts]([Id])
)

GO
