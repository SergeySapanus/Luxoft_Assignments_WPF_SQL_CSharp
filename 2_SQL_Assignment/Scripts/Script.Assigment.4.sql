-- get all supplier names who had not made any deliveries to regions where supplier X has any delivery

select distinct 
	s.Name 
from 
	luxoft.RefSuppliers s
		left join luxoft.DeliveryHeaders dh on dh.IdSupplier = s.Id
		left join luxoft.RefRegions r on r.Id = dh.IdRegion
where r.Id not in 
(
	select r.Id from luxoft.RefRegions r
	inner join luxoft.DeliveryHeaders dh on dh.IdRegion = r.Id
	inner join luxoft.RefSuppliers s on s.Id = dh.IdSupplier
	where s.Name = 'X'
) or r.Id is null