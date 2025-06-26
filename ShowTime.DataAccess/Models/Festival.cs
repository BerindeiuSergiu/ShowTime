using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Models
{
    public class Festival
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public string SplashArt { get; set; } = string.Empty;
        public int Capacity { get; set; }

        // Un festival poate avea mai multe line-up-uri
        public ICollection<Lineup> Lineups { get; set; } = new List<Lineup>();
        public ICollection<Artists> Artists { get; set; } = new List<Artists>();

    }
}
