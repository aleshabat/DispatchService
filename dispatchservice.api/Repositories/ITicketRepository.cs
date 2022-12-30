using System;
using System.Collections.Generic;
using dispatchservice.api.Domain;

namespace dispatchservice.api.Repositories
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        IEnumerable<Ticket> GetList(DateTime? dateStart, DateTime? dateEnd, Guid? injenerId);
        IEnumerable<Ticket> GetList(Guid? houseId, Guid? serviceId, Guid? injenerId, DateTime? dateExecute);
    }
}
