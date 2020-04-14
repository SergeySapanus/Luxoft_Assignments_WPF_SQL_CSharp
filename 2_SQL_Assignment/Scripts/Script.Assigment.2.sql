-- get summary value for all parts supplied by supplier X during January 2000

select 
	sum(dl.Price/* * dl.Number*/) as Summary 
from 
	luxoft.DeliveryLines dl
		inner join luxoft.DeliveryHeaders dh on dh.Id = dl.IdDeliveryHeader
		inner join luxoft.RefSuppliers s on s.Id = dh.IdSupplier
where 
	s.Name = 'X' and 
	dh.Date >= '20000101' and 
	dh.Date < '20000201'