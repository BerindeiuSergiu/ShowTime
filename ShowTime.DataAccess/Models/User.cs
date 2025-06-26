using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Role { get; set; }

        // un user poate avea mai multe rezervari
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        // un user poate avea mai multe festivaluri favorite
        public ICollection<Festival> FavoriteFestivals { get; set; } = new List<Festival>();

    }
}
