select t.Id, t.Number, t.Flat, t.[DateTime], t.DateCancel, t.DateExecute, t.Phone, t.MobilePhone, t.Price, t.HouseId,
		i.Id, i.Name, i.Deleted,
		/*h.Id, h.Number, h.Deleted,
		st.Id, st.Name,
		e.Id, e.Name,*/
		s.Id, s.Name, s.Price, s.Deleted
from Ticket t
left join Injener i on i.Id = t.InjenerId
/*join House h on h.Id = ih.HouseId
join Street st on st.Id = h.StreetId
join Estate e on e.Id = h.EstateId*/
join [Service] s on s.Id = t.ServiceId
