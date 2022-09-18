namespace BlogApi.Dtos
{
    public class TokenDto
    {
        public string Token { get; set; } = string.Empty;
        public ReadUserDto User { get; set; }
    }
}
