select h.Id, h.Number, h.Deleted, 
		s.Id, s.Name, s.Type,
		e.Id, e.Name, e.Type
from House h
join Street s on s.Id = h.StreetId
join Estate e on e.Id = h.EstateId
where (h.Number = @Number or @Number = '') and
		(s.Name) like @Street and 
		e.Name like @Estate