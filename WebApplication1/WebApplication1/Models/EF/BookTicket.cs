using System;
using System.Collections.Generic;

namespace WebApplication1.Models.EF
{
    public partial class BookTicket
    {
        public int PassengerId { get; set; }
        public int? FlightNo { get; set; }
        public string? PassengerFistName { get; set; }
        public string? PassengerLastName { get; set; }
        public string? City { get; set; }
        public int? Age { get; set; }

        public virtual BookFlight? FlightNoNavigation { get; set; }
    }
}
