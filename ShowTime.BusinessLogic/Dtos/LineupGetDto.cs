
namespace ShowTime.BusinessLogic.Dtos
{
    public class LineupGetDto
    {
        public int Id { get; set; }
        public int FestivalId { get; set; }
        public int ArtistId { get; set; }
        public string Stage { get; internal set; }
        public DateTime StartTime { get;  set; }
        // Optionally, add public DateTime? PerformanceTime { get; set; }
    }
} 