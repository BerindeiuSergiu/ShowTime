using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.DataAccess.GenericInterface;
using ShowTime.DataAccess.Models;
using ShowTime.BusinessLogic.Dtos;
using ShowTime.BussinessLogic.Dtos;

namespace ShowTime.BusinessLogic.Services
{
    public class BookingService : IBookingService
    {
        private readonly IGenericRepository<Booking> _bookingRepository;
        private readonly IGenericRepository<Ticket> _ticketRepository;

        public BookingService(IGenericRepository<Booking> bookingRepository, IGenericRepository<Ticket> ticketRepository)
        {
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
        }

        public async Task<bool> CreateBookingAsync(int festivalId, int userId, int ticketId)
        {
            try
            {
                // First, check if the ticket exists and has available quantity
                var ticket = await _ticketRepository.GetByIdAsync(ticketId);
                if (ticket == null)
                {
                    throw new InvalidOperationException("Ticket not found.");
                }

                if (ticket.Quantity <= 0)
                {
                    throw new InvalidOperationException("No tickets available for booking.");
                }

                // Check if user already has a booking for this festival
                var existingBookings = await _bookingRepository.GetAllAsync();
                var userHasBooking = existingBookings.Any(b => b.FestivalId == festivalId && b.UserId == userId);

                if (userHasBooking)
                {
                    throw new InvalidOperationException("You already have a booking for this festival.");
                }

                // Create the booking
                var booking = new Booking
                {
                    FestivalId = festivalId,
                    UserId = userId,
                    TicketId = ticketId
                };

                var createdBooking = await _bookingRepository.AddAsync(booking);

                if (createdBooking != null)
                {
                    // Decrement the ticket quantity
                    ticket.Quantity--;
                    await _ticketRepository.UpdateAsync(ticket);
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while creating a booking.", e);
            }
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId)
        {
            try
            {
                var allBookings = await _bookingRepository.GetAllAsync();
                var userBookings = allBookings.Where(b => b.UserId == userId);
                return userBookings;
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while retrieving bookings for the user.", e);
            }
        }

        public async Task<bool> CancelBookingAsync(int festivalId, int userId)
        {
            try
            {
                var allBookings = await _bookingRepository.GetAllAsync();
                var booking = allBookings.FirstOrDefault(b =>
                    b.FestivalId == festivalId &&
                    b.UserId == userId);

                if (booking == null)
                {
                    return false;
                }

                // Get the ticket to increment its quantity
                var ticket = await _ticketRepository.GetByIdAsync(booking.TicketId);
                if (ticket != null)
                {
                    // Increment the ticket quantity back
                    ticket.Quantity++;
                    await _ticketRepository.UpdateAsync(ticket);
                }

                _bookingRepository.Delete(booking);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while canceling the booking.", e);
            }
        }

        public async Task<IList<BookingGetDto>> GetAllBookingsAsync()
        {
            try
            {
                var bookings = await _bookingRepository.GetAllAsync();
                return bookings.Select(bookings => new BookingGetDto
                {
                    FestivalId = bookings.FestivalId,
                    UserId = bookings.UserId,
                    TicketId = bookings.TicketId
                }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while retrieving all bookings.", e);
            }
        }
    }
}