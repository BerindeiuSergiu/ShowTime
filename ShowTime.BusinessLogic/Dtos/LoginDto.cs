namespace ShowTime.BusinessLogic.Dtos
{
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        // Optionally, include Password only for login, not for returning user info
        public string? Password { get; set; } // Only for login
    }
} 