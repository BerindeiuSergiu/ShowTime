using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace ShowTime.DataAccess.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string TicketType { get; set; } = string.Empty;

        public Booking Booking { get; set; } = null!;
    }
}
