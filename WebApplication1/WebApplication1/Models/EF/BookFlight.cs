using System;
using System.Collections.Generic;

namespace WebApplication1.Models.EF
{
    public partial class BookFlight
    {
        public BookFlight()
        {
            BookTickets = new HashSet<BookTicket>();
        }

        public int FlightNo { get; set; }
        public string? Source { get; set; }
        public string? Destination { get; set; }
        public int? Fare { get; set; }
        public int? TotalSeats { get; set; }

        public virtual ICollection<BookTicket> BookTickets { get; set; }
    }
}
