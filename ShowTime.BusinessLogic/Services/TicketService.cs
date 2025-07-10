using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Dtos;
using ShowTime.DataAccess.GenericInterface;
using ShowTime.DataAccess.Models;

namespace ShowTime.BusinessLogic.Services
{
    public class TicketService : ITicketService
    {
        private readonly IGenericRepository<Ticket> _ticketRepository;
        public TicketService(IGenericRepository<Ticket> ticketRepository)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
        }

        public async Task<TicketGetDto> GetByIDAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null)
                throw new KeyNotFoundException($"Ticket with ID {id} not found.");
            return new TicketGetDto
            {
                Id = ticket.Id,
                Price = ticket.Price,
                TicketType = ticket.TicketType,
                Quantity = ticket.Quantity,
                FestivalId = ticket.FestivalId
            };
        }

        public async Task<IList<TicketGetDto>> GetAllTicketsAsync()
        {
            var tickets = await _ticketRepository.GetAllAsync();
            return tickets.Select(ticket => new TicketGetDto
            {
                Id = ticket.Id,
                Price = ticket.Price,
                TicketType = ticket.TicketType,
                Quantity = ticket.Quantity,
                FestivalId = ticket.FestivalId
            }).ToList();
        }

        public async Task<Ticket> AddTicketAsync(TicketCreateDto ticketCreateDto)
        {
            var ticket = new Ticket
            {
                Price = ticketCreateDto.Price,
                TicketType = ticketCreateDto.TicketType,
                Quantity = ticketCreateDto.Quantity,
                FestivalId = ticketCreateDto.FestivalId
            };
            return await _ticketRepository.AddAsync(ticket);
        }

        public async Task<Ticket> DeleteTicketAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null)
                throw new KeyNotFoundException($"Ticket with ID {id} not found.");
            _ticketRepository.Delete(ticket);
            return ticket;
        }

        public async Task<Ticket> UpdateTicketAsync(TicketUpdateDto ticketUpdateDto)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketUpdateDto.Id);
            if (ticket == null)
                throw new KeyNotFoundException($"Ticket with ID {ticketUpdateDto.Id} not found.");
            ticket.Price = ticketUpdateDto.Price;
            ticket.TicketType = ticketUpdateDto.TicketType;
            ticket.Quantity = ticketUpdateDto.Quantity;
            return await _ticketRepository.UpdateAsync(ticket);
        }


        public async Task<Ticket> UpdateQuantity(TicketUpdateDto ticketUpdateDto)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketUpdateDto.Id);

            if(ticket == null)
                throw new KeyNotFoundException($"Ticket with ID {ticketUpdateDto.Id} not found to change price.");

            return await _ticketRepository.UpdateAsync(ticket);

        }

    }
} 