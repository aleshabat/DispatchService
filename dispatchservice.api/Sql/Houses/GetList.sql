select h.Id, h.Number, h.Deleted, s.Id, s.Name, s.Type,
		e.Id, e.Name, e.Type
from House h
join Street s on s.Id = h.StreetId
join Estate e on e.Id = h.EstateId
where h.StreetId = @StreetId and (h.EstateId = @EstateId or @EstateId is null)