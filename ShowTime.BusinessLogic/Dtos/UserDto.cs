namespace ShowTime.BusinessLogic.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public int Role { get; set; }
        // Optionally, include Password only for login, not for returning user info
        public string? Password { get; set; } // Only for login
    }
} 