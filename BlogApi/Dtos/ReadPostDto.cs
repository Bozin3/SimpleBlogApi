namespace BlogApi.Dtos
{
    public class ReadPostDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public ReadUserDto User { get; set; }
    }
}
