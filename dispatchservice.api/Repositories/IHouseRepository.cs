using System;
using System.Collections.Generic;
using dispatchservice.api.Domain;

namespace dispatchservice.api.Repositories
{
    public interface IHouseRepository : IRepository<House>
    {
        IEnumerable<House> Find(string searchString);
        IEnumerable<House> GetList(Guid streetId, Guid? estateId);

    }
}
