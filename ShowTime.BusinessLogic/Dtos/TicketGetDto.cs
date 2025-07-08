using System;

namespace ShowTime.BusinessLogic.Dtos
{
    public class TicketGetDto
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string TicketType { get; set; } = string.Empty;
    }
} 