using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.DataAccess.GenericRepository;
using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.SpecificRepositories
{
    public class ArtistRepository : GenericRepository<Artists>
    {
        public ArtistRepository() { }
        // implement specific methods for Artist if needed
        /*
         * deci daca e nevoie de alte operatii mai smechere aici vin adaugate
         * astea pot fi facute pentru orice entitate am eu salvata
         */
    }
}
