select h.Id, h.Number, h.Deleted, h.AOGUID, h.HOUSENUM, h.BUILDNUM, h.STRUCNUM, h.ESTSTATUS, h.STRSTATUS,
		s.Id, s.Name, s.Type,
		e.Id, e.Name, e.Type
from House h
join Street s on s.Id = h.StreetId
left join Estate e on e.Id = h.EstateId