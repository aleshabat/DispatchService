using System;
using System.Collections.Generic;
using dispatchservice.api.Domain;

namespace dispatchservice.api.Repositories
{
    public interface IInjenerHouseRepository : IRepository<InjenerHouse>
    {
        IEnumerable<InjenerHouse> GetList(Guid houseId);
        IEnumerable<InjenerHouse> GetList(Guid injenerId, Guid? estateId);
        void Delete(Guid injenerId, Guid? estateId);
    }
}
