using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Models
{
    public class Booking
    {
        public int FestivalId { get; set; }
        public int UserId { get; set; }
        public int TicketId { get; set; }

        //pt a realiza legatura
        /*
         * proprietate pentru naigare EF Core
         * permite acces in mod object-oriented eg. user.Role.Name (considerand ca in Role ai prop. Name)
         * 
         */
        public Festival Festival { get; set; } = null; 
        public User User { get; set; } = null;

        public Ticket Ticket { get; set; } = null;

    }
}
