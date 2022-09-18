namespace BlogApi.Dtos
{
    public class RegisterUserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; }
    }
}
