using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.DataAccess.GenericInterface;
using ShowTime.DataAccess.Models;
using ShowTime.BusinessLogic.Dtos;

namespace ShowTime.BusinessLogic.Services
{
    public class BookingService : IBookingService
    {
        private readonly IGenericRepository<Booking> _bookingRepository;

        public BookingService(IGenericRepository<Booking> bookingRepository)
        {
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        }

        public async Task<bool> CreateBookingAsync(int festivalId, int userId, string type)
        {
            try
            {
                var booking = new Booking
                {
                    FestivalId = festivalId,
                    UserId = userId,
                    Type = type,
                    Price = 0 // Assuming price is set to 0 initially, you can modify this as needed  
                };

                var createdBooking = await _bookingRepository.AddAsync(booking);
                return createdBooking != null; // Check if the booking was successfully created  
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while creating a booking.", e);
            }
        }

        public Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId)
        {
            try
            {
                var bookings = _bookingRepository.GetAllAsync().Result
                    .Where(b => b.UserId == userId);

                return Task.FromResult(bookings);
            }
            catch(Exception e)
            {
                throw new Exception("An error occurred while retrieving bookings for the user.", e);
            }
        }

    }
}
