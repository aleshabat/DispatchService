using System;
using System.Linq;
using DapperExtensions.Mapper;

namespace dispatchservice.api.Domain.Maps
{
    public class ProdAutoClassMapper<T> : AutoClassMapper<T> where T : class
    {
        protected override void AutoMap()
        {
            AutoMap((type, pi) => !FindType<Dict>(pi.PropertyType) &&
                                  !FindType<Entity>(pi.PropertyType) &&
                                  !pi.GetCustomAttributes(typeof(NotMapedAttribute), true).Any());

            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                if (Properties.Any(p => p.Name.Equals(propertyInfo.Name, StringComparison.InvariantCultureIgnoreCase)))
                {
                    continue;
                }
                Map(propertyInfo).Ignore();
            }
        }

        bool FindType<T>(Type type)
        {
            while (type != null)
            {
                if (type == typeof(T))
                    return true;

                type = type.BaseType;
            }
            return false;
        }

    }

}