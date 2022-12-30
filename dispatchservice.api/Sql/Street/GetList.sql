select distinct s.Id, s.Name, s.Type, s.AOGUID
from Street s
join House h on h.StreetId = s.Id
where h.EstateID = @EstateID


