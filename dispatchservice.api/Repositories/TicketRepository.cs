using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using dispatchservice.api.Domain;
using dispatchservice.api.Sql;

namespace dispatchservice.api.Repositories
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
       public override IEnumerable<Ticket> All()
        {
            return Select(Queries.Tickets.All, null);
        }

        public override Ticket Get(object id)
        {
            return Select(Queries.Tickets.Get, new {Id = id}).FirstOrDefault(); 
        }

        public IEnumerable<Ticket> GetList(DateTime? dateStart, DateTime? dateEnd, Guid? injenerId)
        {
            return Select(Queries.Tickets.GetByDateRange, new{ DateStart = dateStart, DateEnd = dateEnd, InjenerId = injenerId });
        }

        public IEnumerable<Ticket> GetList(Guid? houseId, Guid? serviceId, Guid? injenerId, DateTime? dateExecute)
        {
            return Select(Queries.Tickets.GetByHouse, new { HouseId = houseId, ServiceId = serviceId, InjenerId = injenerId, DateExecute = dateExecute });
        }

        //Больше 5 таблиц в запросе почему то не мапится
        private IEnumerable<Ticket> Select(string query, object args)
        {
            var repHouse = new HouseRpository { Connection = Connection };
            var tickets = Connection.Query<Ticket, Injener,/* House, Street, Estate,*/ Service, Ticket>(query, (ticket, injener,/* house, street, estate,*/ service) =>
            {
                //ticket.InjenerHouse.House = house;
                //ticket.InjenerHouse.House.Street = street;
               //ticket.InjenerHouse.House.Estate = estate;
                ticket.Injener = injener;
                ticket.Service = service;
                return ticket;

            }, args);

            foreach (var ticket in tickets)
                ticket.House = repHouse.Get(ticket.HouseId);

            return tickets;
        }
    }
}
