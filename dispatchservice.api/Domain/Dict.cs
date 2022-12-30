using System;
using dispatchservice.api.Domain.Maps;

namespace dispatchservice.api.Domain
{
    public class Dict : Entity<Guid>
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
