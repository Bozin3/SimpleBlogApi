namespace BlogApi.Dtos
{
    public class CreatePostDto
    {
        public string Content { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
