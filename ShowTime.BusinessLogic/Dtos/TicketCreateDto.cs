using System;

namespace ShowTime.BusinessLogic.Dtos
{
    public class TicketCreateDto
    {
        public int Price { get; set; }
        public string TicketType { get; set; } = string.Empty;
    }
} 