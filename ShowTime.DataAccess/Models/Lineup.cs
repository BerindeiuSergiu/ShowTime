using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Models
{
    public class Lineup
    {
        public int FestivalID { get; set; }
        public int ArtistID { get; set; }
        public string Stage { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }


        // proprietati pentru legatura
        public Festival Festival { get; set; } = null;
        public Artists Artist { get; set; } = null;





    }
}
