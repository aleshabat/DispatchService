select t.Id, t.Number, t.Flat, t.[DateTime], t.DateCancel, t.DateExecute, t.Phone, t.MobilePhone, t.Price,
		t.InjenerId, t.HouseId,
		i.Id, i.Name, i.Deleted,
		s.Id, s.Name, s.Price, s.Deleted
from Ticket t
left join Injener i on i.Id = t.InjenerId
join [Service] s on s.Id = t.ServiceId
where (t.[DateTime] >= Datetime(@DateStart) or @DateStart is null) and
		(t.[DateTime] <= Datetime(@DateEnd) or @DateEnd is null) and
		(t.InjenerId = @InjenerId or @InjenerId is null)
