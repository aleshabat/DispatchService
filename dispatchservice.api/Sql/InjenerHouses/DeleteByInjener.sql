delete from InjenerHouse 
where Id in(
	select ih.Id
	from InjenerHouse ih
	join House h on h.Id = ih.HouseId
	join Estate e on e.Id = h.EstateId
	where ih.InjenerId = @InjenerId and (h.EstateId = @EstateId or @EstateId is null)
	)