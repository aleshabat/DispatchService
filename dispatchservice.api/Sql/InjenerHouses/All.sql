select ih.Id,
		i.Id, i.Name, i.Deleted,
		h.Id, h.Number, h.Deleted,
		s.Id, s.Name, s.Type,
		e.Id, e.Name, e.Type
from InjenerHouse ih
join Injener i on i.Id = ih.InjenerId
join House h on h.Id = ih.HouseId
join Street s on s.Id = h.StreetId
join Estate e on e.Id = h.EstateId