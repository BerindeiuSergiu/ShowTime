using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.DataAccess.Models;

namespace ShowTime.BusinessLogic.Abstractions
{
    public interface IBookingService
    {

        Task<bool> CreateBookingAsync(int festivalId, int userId, string type);
        Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId);
    }
}
