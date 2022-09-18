using BlogApi.Data.Entities;

namespace BlogApi.Dtos
{
    public class UpdatePostDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;

        public BlogPost UpdateBlogPost(BlogPost post)
        {
            post.Content = Content;
            return post;
        }
    }
}
