-- get number of deliveries with total cost more than 1000 USD 
-- (total cost is sum of price times number of parts by all lines for this delivery), 
-- grouped by supplier (result table should have columns Supplier name and Delivery number)

-- number as item
select 
	s.Name as 'Supplier name', 
	dh.Id as 'Delivery number' 
from 
	luxoft.DeliveryLines dl
		inner join luxoft.DeliveryHeaders dh on dh.Id = dl.IdDeliveryHeader
		inner join luxoft.RefSuppliers s on s.Id = dh.IdSupplier
		inner join luxoft.RefCurrencies c on c.Id = dh.IdCurrency
where 
	c.ShortName = 'USD'
group by 
	s.Name, 
	dh.Id
having 
	sum(dl.Price * dl.Number) >= 1000
order by 
	s.Name

-- number as amount
select 
	s.Name as 'Supplier name', 
	count(dh.Id) as 'Delivery number'
from 
	luxoft.RefSuppliers s
		inner join luxoft.DeliveryHeaders dh on dh.IdSupplier = s.Id
		inner join luxoft.RefCurrencies c on c.Id = dh.IdCurrency
where 
	c.ShortName = 'USD' and
	dh.id in 
	(
		select dl.IdDeliveryHeader 
		from luxoft.DeliveryLines dl 			
		group by dl.IdDeliveryHeader
		having sum(dl.Price * dl.Number) >= 1000
	)
group by 
	s.Name
order by 
	s.Name