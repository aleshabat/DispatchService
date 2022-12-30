using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using dispatchservice.api.Domain;
using dispatchservice.api.Sql;
using System.Text.RegularExpressions;

namespace dispatchservice.api.Repositories
{
    public class HouseRpository : Repository<House> , IHouseRepository
    {
        public override IEnumerable<House> All()
        {
            return Select(Queries.Houses.All, null);
        }

        public override House Get(object id)
        {
            return Select(Queries.Houses.Get, new {Id = id}).FirstOrDefault(); 
        }

        public IEnumerable<House> Find(string searchString)
        {
            string number = "", street = "", estate = "";

            searchString = searchString.Replace(", ", " ")
                .Replace(" д.", " ")
                .Replace(" д ", " ")
                .Replace(" ул.", " ")
                .Replace(" ул ", " ")
                .Replace("ул ", " ")
                .Replace("ул. ", " ")
                .Trim();

            var regex1 = new Regex(@"[^\s]+", RegexOptions.IgnoreCase);
            var words = regex1.Matches(searchString);

            if (searchString != "")
            {
                if (words.Count > 1)
                {
                    number = words[words.Count - 1].Value;
                    for (int i = 0; i <= words.Count - 2; i++)
                        street = street + "%" + words[i].Value;

                    street = street + "%";
                }
                else
                {
                    street = "%" + searchString + "%";
                }

                return Select(Queries.Houses.Find, new
                {
                    Number = number,
                    Street = street,
                    Estate = "%" + estate + "%"
                });
            }
                return new List<House>();

        }

        public IEnumerable<House> GetList(Guid streetId, Guid? estateId)
        {
            return Select(Queries.Houses.GetList, new { StreetId = streetId, EstateId = estateId }); 
        }

        private IEnumerable<House> Select(string query, object args)
        {
            return Connection.Query<House, Street, Estate, House>(query, (house, street, estate) =>
            {
                house.Street = street;
                house.Estate = estate;
                return house;

            }, args);
        }
    }
}
