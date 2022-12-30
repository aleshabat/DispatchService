select 	i.Name as InjenerName,
		s.Name as ServiceName,
		sum(t.Price) as Summa,
		count(t.id) as TicketCount
from Ticket t
left join Injener i on i.Id = t.InjenerId
join [Service] s on s.Id = t.ServiceId
where t.[DateExecute] >= Datetime(@DateStart) and
		t.[DateExecute] <= Datetime(@DateEnd)
group by t.InjenerId, i.Name, t.ServiceId, s.Name
order by i.Name, s.Name
