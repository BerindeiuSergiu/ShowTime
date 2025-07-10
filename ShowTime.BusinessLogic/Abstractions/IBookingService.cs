using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.BusinessLogic.Dtos;
using ShowTime.DataAccess.Models;

namespace ShowTime.BusinessLogic.Abstractions
{
    public interface IBookingService
    {

        Task<bool> CreateBookingAsync(int festivalId, int userId, int ticketId);
        Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId);
        Task<bool> CancelBookingAsync(int festivalId, int userId);
        Task<IList<BookingGetDto>> GetAllBookingsAsync();
    }
}
