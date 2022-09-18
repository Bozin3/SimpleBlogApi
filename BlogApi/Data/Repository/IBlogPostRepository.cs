using BlogApi.Data.Entities;

namespace BlogApi.Data.Repository
{
    public interface IBlogPostRepository
    {
        Task<List<BlogPost>> GetBlogPosts();
        Task<BlogPost?> GetBlogPostById(int id);
        Task CreatePost(BlogPost post);
        Task UpdateBlogPost(BlogPost blogPost);
        Task DeleteBlogPost(BlogPost post);
    }
}
