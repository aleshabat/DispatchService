using System;
using System.Collections.Generic;
using Dapper;
using dispatchservice.api.Domain;
using dispatchservice.api.Sql;

namespace dispatchservice.api.Repositories
{
    public class StreetRepository : Repository<Street>, IStreetRepository
    {

        public IEnumerable<Street> GetList(Guid estateId)
        {
            return Select(Queries.Street.GetList, new { EstateId = estateId }); 
        }

        private IEnumerable<Street> Select(string query, object args)
        {
            return Connection.Query<Street>(query, args);
        }
    }
}
