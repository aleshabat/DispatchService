using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using dispatchservice.api.Domain;
using dispatchservice.api.Repositories;

namespace dispatchservice.api.Services
{
    public class TicketService
    {
        public IDbConnection Connection { get; set; }
        private ITicketRepository _ticketRepository;

        public TicketService(IDbConnection connection)
        {
            Connection = connection;
            _ticketRepository = new TicketRepository { Connection = Connection };
        }

        public Injener GetInjenerForNewTicket(Guid serviceId, Guid houseId, DateTime dateExecute)
        {
            IInjenerHouseRepository injenerHouseRepository = new InjenerHouseRepository();

            injenerHouseRepository.Connection = Connection;
            _ticketRepository.Connection = Connection;

            var tickets = _ticketRepository.GetList(houseId, serviceId, null, dateExecute); 
            if (!tickets.Any())
                return null;

            return tickets.Select( t => t.Injener).First();
        }

        public DateTime? GetDateExecuteForNewTicket(Guid serviceId, Guid houseId)
        {
            List<Ticket> tickets = new List<Ticket>();
            if (serviceId != Guid.Empty && houseId != Guid.Empty)
            {
                tickets = _ticketRepository.GetList(houseId, serviceId, null, null).ToList();
            }

            if (!tickets.Any())
                return null;

            return tickets.OrderBy(t => t.DateExecute).First().DateExecute;
        }
    }
}
