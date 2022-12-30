using System;

namespace dispatchservice.api.Domain
{
    public class InjenerHouse : Entity<Guid> 
    {
        public InjenerHouse()
        {
            Injener = new Injener();
            House = new House();
        }

        public Injener Injener { get; set; }
        public Guid InjenerId
        {
            get { return Injener.Id; }
            set { Injener.Id = value; }
        }

        public House House { get; set; }
        public Guid HouseId
        {
            get { return House.Id; }
            set { House.Id = value; }
        }
    }
}