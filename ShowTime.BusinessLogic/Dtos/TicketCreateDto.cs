using System;

namespace ShowTime.BusinessLogic.Dtos
{
    public class TicketCreateDto
    {
        public int Price { get; set; }
        public string TicketType { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int FestivalId { get; set; }
    }
} 