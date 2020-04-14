-- get list of suppliers’ names that made at least one delivery to region with name A

select 
	s.Name
from 
	luxoft.RefSuppliers s
		inner join luxoft.DeliveryHeaders dh on dh.IdSupplier = s.Id
		inner join luxoft.RefRegions r on dh.IdRegion = r.Id
where 
	r.Name = 'A'
group by 
	s.Name