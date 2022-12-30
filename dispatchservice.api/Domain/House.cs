
using System;
using dispatchservice.api.Domain.Maps;

namespace dispatchservice.api.Domain
{
    public class House : Entity<Guid> 
    {
        public House()
        {
            Street = new Street();
            Estate = new Estate();
        }

        public string Number { get; set; }
        public override string ToString()
        {
            return Number;
        }

        public Street Street { get; set; }
        public Guid StreetId
        {
            get { return Street.Id; }
            set { Street.Id = value; }
        }

        public Estate Estate { get; set; }
        public Guid? EstateId
        {
            get
            {
                return Estate != null ? Estate.Id :  (Guid?) null;
            }
            set
            {
                if (value != null)
                    Estate.Id = (Guid)value;
                else
                    Estate = null;
            }
        }

        public bool Deleted { get; set; }

        public Guid? AOGUID { get; set; }
        public string HOUSENUM { get; set; }
        public string BUILDNUM { get; set; }
        public string STRUCNUM { get; set; }
        public Int16? ESTSTATUS { get; set; }
        public Int16? STRSTATUS { get; set; }

        [NotMaped]
        public string FullAddress
        {
            get{ return string.Format("{0} {1} д. {2}", Estate.FullName, Street.FullName, Number); }
        }
        
    }
} 