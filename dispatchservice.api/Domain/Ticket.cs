
using System;
using System.ComponentModel;

namespace dispatchservice.api.Domain
{
    public class Ticket : Entity<Guid> 
    {
        public Ticket()
        {
            Service = new Service();
            Injener = new Injener();
            House = new House();
            Phone = "";
            MobilePhone = "";
        }

        public int Number { get; set; }

        public Service Service { get; set; }
        public Guid ServiceId
        {
            get { return Service.Id; }
            set { Service.Id = value; }
        }

        public Injener Injener { get; set; }
        public Guid? InjenerId
        {
            get { return Injener != null ? (Guid?)Injener.Id : null; }
            set
            {
                if (value != null)
                    Injener.Id = (Guid) value;
                else
                    Injener = null;
            }
        }

        public House House { get; set; }
        public Guid HouseId
        {
            get { return House.Id; }
            set { House.Id = value; }
        }

        public string Flat { get; set; }

        public DateTime DateTime { get; set; }
        public DateTime? DateCancel { get; set; }
        public DateTime? DateExecute { get; set; }

        // WithDefaultValue в миграторе не работает, пришлось делать так
        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { _phone = value ?? ""; }
        }

        // WithDefaultValue в миграторе не работает, пришлось делать так
        private string _mobilePhone;
        public string MobilePhone
        {
            get { return _mobilePhone; }
            set { _mobilePhone = value ?? ""; }
        }

        public decimal Price { get; set; }
    }
} 