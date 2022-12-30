using System;
using System.Data;
using System.Linq;
using dispatchservice.api.Domain;
using dispatchservice.api.Repositories;

namespace dispatchservice.api.Services
{
    public class StreetService
    {
        public IDbConnection Connection { get; set; }
        private readonly IStreetRepository _streetRepository;
        public IHouseRepository HouseRepository { get; set; }
        private readonly ITicketRepository _ticketRepository;

        public StreetService(IDbConnection connection)
        {
            Connection = connection;
            _streetRepository = new StreetRepository { Connection = Connection };
            HouseRepository = new HouseRpository { Connection = Connection };
            _ticketRepository = new TicketRepository { Connection = Connection };

        }

        /// <summary>
        /// Удаляет дом
        /// </summary>
        /// <param name="house"></param>
        /// <returns> true - удален из БД, false - установлен признак Deleted </returns>
        public bool DeleteHouse(House house)
        {
            if (HasTickets(house.Id))
            {
                house.Deleted = true;
                HouseRepository.Update(house);
                return false;
            }
            HouseRepository.Delete(house.Id);
            return true;
        }

        private bool HasTickets(Guid houseId)
        {
            var tickets = _ticketRepository.GetList(houseId, null, null, null).ToList();
            return tickets.Any();
        }
    }
}
