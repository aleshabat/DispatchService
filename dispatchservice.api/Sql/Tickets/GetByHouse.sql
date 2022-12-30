select t.Id, t.Number, t.Flat, t.[DateTime], t.DateCancel, t.DateExecute, t.Phone, t.MobilePhone, t.Price,
		t.InjenerId, t.HouseId,
		i.Id, i.Name, i.Deleted,
		s.Id, s.Name, s.Price, s.Deleted
from Ticket t
left join Injener i on i.Id = t.InjenerId
join [Service] s on s.Id = t.ServiceId
where (t.HouseId = @HouseId or @HouseId is null) and
		(t.ServiceId = @ServiceId or @ServiceId is null) and
		(t.DateExecute = Datetime(@DateExecute) or @DateExecute is null) and
		(t.InjenerId = @InjenerId or @InjenerId is null)
