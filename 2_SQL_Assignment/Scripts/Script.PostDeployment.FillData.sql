--luxoft.RefCurrencies
with data as
(
	select *
	from (values
	(newid(), N'United States dollar',  'USD'),
	(newid(), N'Euro', 'EUR'),
	(newid(), N'Ukrainian hryvnia', 'UAH')
	) as T(Id, Name, ShortName)
)

merge luxoft.RefCurrencies as tgt
using data as src
on tgt.ShortName = src.ShortName
when not matched by target 
then
	insert (Id, Name, ShortName)
	values (Id, Name, ShortName);

go

--luxoft.RefMachineParts
with data as
(
	select *
	from (values
	(newid(), N'Shafts'),
	(newid(), N'Coupling'),
	(newid(), N'Key'),
	(newid(), N'Spline'),
	(newid(), N'Bearing'),
	(newid(), N'Gears'),
	(newid(), N'Fastener'),
	(newid(), N'Belt'),
	(newid(), N'Clutch'),
	(newid(), N'Brake'),
	(newid(), N'Chain'),
	(newid(), N'Wire rope')
	) as T(Id, Name)
)

merge luxoft.RefMachineParts as tgt
using data as src
on tgt.Name = src.Name
when not matched by target 
then
	insert (Id, Name)
	values (Id, Name);

go

--luxoft.RefRegions
with data as
(
	select *
	from (values
	(newid(), N'Alabama'),
	(newid(), N'Alaska'),
	(newid(), N'American Samoa'),
	(newid(), N'Arizona'),
	(newid(), N'Arkansas'),
	(newid(), N'California'),
	(newid(), N'Colorado'),
	(newid(), N'Connecticut'),
	(newid(), N'Delaware'),
	(newid(), N'District of Columbia'),
	(newid(), N'Florida'),
	(newid(), N'Georgia'),
	(newid(), N'Guam'),
	(newid(), N'Hawaiʻi'),
	(newid(), N'Idaho'),
	(newid(), N'Illinois'),
	(newid(), N'Indiana'),
	(newid(), N'Iowa'),
	(newid(), N'Kansas'),
	(newid(), N'Kentucky'),
	(newid(), N'Louisiana'),
	(newid(), N'Maine'),
	(newid(), N'Maryland'),
	(newid(), N'Massachusetts')
	) as T(Id, Name)
)

merge luxoft.RefRegions as tgt
using data as src
on tgt.Name = src.Name
when not matched by target 
then
	insert (Id, Name)
	values (Id, Name);

go

--luxoft.RefSuppliers
with data as
(
	select *
	from (values
	(newid(), N'Guangzhou Jinyu Auto Parts Co., Ltd.'),
	(newid(), N'Guangzhou Comma Car Care Accessories Co., Ltd.'),
	(newid(), N'Qingzhi Car Parts Co., Ltd.'),
	(newid(), N'Guangzhou Lanbo Car Accessories Co., Ltd.'),
	(newid(), N'Guangzhou AES Car Parts Co., Ltd.'),
	(newid(), N'Guangzhou Power Car Auto Accessories Co., Ltd.'),
	(newid(), N'Ningbo Ocean Car Accessories Co., Ltd.'),
	(newid(), N'Shenzhen Anda Cars Parts Co., Ltd.'),
	(newid(), N'SINGAPURA CARS & PARTS PTE. LTD.')
	) as T(Id, Name)
)

merge luxoft.RefSuppliers as tgt
using data as src
on tgt.Name = src.Name
when not matched by target 
then
	insert (Id, Name)
	values (Id, Name);

go

-- truncate luxoft.DeliveryLines & luxoft.DeliveryHeaders
truncate table luxoft.DeliveryLines;
delete from luxoft.DeliveryHeaders;

go

--luxoft.DeliveryHeaders
declare @currencyId uniqueidentifier;

if not exists(select Id from luxoft.RefCurrencies where ShortName = 'USD')
begin
	insert into luxoft.RefCurrencies (Id, Name, ShortName)
	values (NEWID(), N'United States dollar',  'USD');
end;

select @currencyId = Id from luxoft.RefCurrencies where ShortName = 'USD';

with data as
(
	select 
		Id,
		IdRegion,
		IdSupplier,
		case 
			when sns.RowId % 5 = 1 then getdate() + 1
			when sns.RowId % 5 = 2 then getdate() - 1 
			when sns.RowId % 5 = 3 then getdate() + 10 
			when sns.RowId % 5 = 4 then getdate() - 10 
			else getdate()
		end Date
	from 
	(
		select 
			row_number() over(order by r.Id asc) as RowId, 
			newid() as Id, 
			r.Id as IdRegion, 
			s.Id as IdSupplier
		from luxoft.RefSuppliers s cross join luxoft.RefRegions r
	) as sns
)

merge luxoft.DeliveryHeaders as tgt
using data as src
on tgt.IdRegion = src.IdRegion and tgt.IdSupplier = src.IdSupplier
when not matched by target 
then
	insert (Id, IdRegion, IdSupplier, IdCurrency, Date)
	values (Id, IdRegion, IdSupplier, @currencyId, Date);

go

--luxoft.DeliveryLines
declare 
	@IdDeliveryHeader uniqueidentifier, 
	@ModuloRowId tinyint,
	@MachinePartsHalfQuantity int

select @MachinePartsHalfQuantity = (count(id) / 2) from  luxoft.RefMachineParts

declare headers cursor for
	 select 
			Id,
			RowId % 2 as ModuloRowId
		from 
		(
			select 
				dh.Id, 
				row_number() over(order by dh.Id asc) as RowId
			from luxoft.DeliveryHeaders dh 
		) as sns

open headers

fetch next from headers into @IdDeliveryHeader, @ModuloRowId

while @@fetch_status = 0
begin
	fetch next from headers into @IdDeliveryHeader, @ModuloRowId

	if @@fetch_status = -1
		break;

	with data as
	(	
		select *
		from 
		(
			select 
				newid() as Id, 
				@IdDeliveryHeader as IdDeliveryHeader,
				mp.Id as IdMachinePart
			from luxoft.RefMachineParts mp 
			order by mp.Id
			offset (@MachinePartsHalfQuantity * @ModuloRowId) row
		) as sns
	)
	merge luxoft.DeliveryLines as tgt
	using data as src
		on tgt.Id = src.Id
	when not matched by target 
	then
		insert (Id, IdDeliveryHeader, IdMachinePart, Number, Price)
		values (Id, IdDeliveryHeader, IdMachinePart, convert(numeric(18, 0), RAND() * (@ModuloRowId + 1) * 10), convert(money, RAND() * 100) * (@ModuloRowId + 1));

end

close headers
deallocate headers