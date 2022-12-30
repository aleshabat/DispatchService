using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using dispatchservice.api.Domain;
using dispatchservice.api.Sql;
using System.Linq;

namespace dispatchservice.api.Repositories
{
    public class InjenerHouseRepository : Repository<InjenerHouse>, IInjenerHouseRepository
    {
        public override IEnumerable<InjenerHouse> All()
        {
            return Select(Queries.InjenerHouses.All, null);
        }

        public override InjenerHouse Get(object id)
        {
            return Select(Queries.InjenerHouses.Get, new { Id = id }).FirstOrDefault(); 
        }

        public IEnumerable<InjenerHouse> GetList(Guid houseId)
        {
            return Select(Queries.InjenerHouses.GetByHouse, new { HouseId = houseId }); 
        }

        public IEnumerable<InjenerHouse> GetList(Guid injenerId, Guid? estateId)
        {
            return Select(Queries.InjenerHouses.GetByInjener, new { InjenerId = injenerId, EstateId = estateId }); 

        }

        public void Delete(Guid injenerId, Guid? estateId)
        {
            Connection.Query(Queries.InjenerHouses.DeleteByInjener,
                                new {InjenerId = injenerId, EstateId = estateId}
                             );
        }

        private IEnumerable<InjenerHouse> Select(string query, object args)
        {
            return Connection.Query<InjenerHouse, Injener, House, Street, Estate, InjenerHouse>(query, (injenerHouse, injener, house, street, estate) =>
            {
                injenerHouse.Injener = injener;
                injenerHouse.House = house;
                injenerHouse.House.Street = street;
                injenerHouse.House.Estate = estate;
                return injenerHouse;

            }, args);
        }
    }
}
