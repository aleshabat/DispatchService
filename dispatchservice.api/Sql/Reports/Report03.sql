select t.DateExecute, t.Phone, t.MobilePhone, t.Price, t.Flat,
		i.Name as InjenerName,
		e.Type as EstateType, e.Name as EstateName,
		st.Type as StreetType, st.Name as StreetName,
		h.Number as HouseNumber,
		s.Name as ServiceName
from Ticket t
left join Injener i on i.Id = t.InjenerId
join House h on h.Id = t.HouseId
join Estate e on e.Id = h.EstateId
join Street st on st.Id = h.StreetId
join [Service] s on s.Id = t.ServiceId
where t.[DateExecute] >= Datetime(@DateExecuteStart) and
		t.[DateExecute] <= Datetime(@DateExecuteEnd)
order by t.DateExecute, i.Name, s.Name, e.Name, st.Name, h.Number, t.Flat
