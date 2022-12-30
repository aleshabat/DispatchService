using System;
using System.Collections.Generic;
using dispatchservice.api.Domain;

namespace dispatchservice.api.Repositories
{
    public interface IStreetRepository : IRepository<Street>
    {
        IEnumerable<Street> GetList(Guid estateId);

    }
}
