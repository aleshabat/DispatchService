select distinct s.Id, s.Name, s.Type
from Street s
join House h on h.StreetId = s.Id
where h.EstateID = @EstateID


