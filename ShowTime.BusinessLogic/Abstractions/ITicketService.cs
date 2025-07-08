using System.Collections.Generic;
using System.Threading.Tasks;
using ShowTime.BusinessLogic.Dtos;
using ShowTime.DataAccess.Models;

namespace ShowTime.BusinessLogic.Abstractions
{
    public interface ITicketService
    {
        Task<TicketGetDto> GetByIDAsync(int id);
        Task<IList<TicketGetDto>> GetAllTicketsAsync();
        Task<Ticket> AddTicketAsync(TicketCreateDto ticketCreateDto);
        Task<Ticket> DeleteTicketAsync(int id);
        Task<Ticket> UpdateTicketAsync(TicketUpdateDto ticketUpdateDto);
    }
} 