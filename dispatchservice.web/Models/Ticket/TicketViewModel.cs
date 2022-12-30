using System;

namespace dispatchservice.web.Models.Ticket
{
    public class TicketViewModel
    {
        public Guid TicketId { get; set; }

        public int Number { get; set; }

        public string Service { get; set; }

        public string Injener { get; set; }

        public string AddressHouse { get; set; }

        public string Flat { get; set; }

        public string DateTime { get; set; }

        public string Phone { get; set; }

        public string MobilePhone { get; set; }

        public decimal Price { get; set; }

        public bool IsCanceled { get; set; }

        public bool IsExecuted { get; set; }

        public bool AllowDeleted { get; set; }
    }
} 