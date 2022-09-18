using BlogApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Data.Repository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogDbContext _blogDbContext;

        public BlogPostRepository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public async Task<List<BlogPost>> GetBlogPosts()
        {
            return await _blogDbContext.Posts.Include(p => p.User).OrderByDescending(p => p.CreatedAt).ToListAsync();
        }

        public async Task<BlogPost?> GetBlogPostById(int id)
        {
            return await _blogDbContext.Posts.Where(p => p.Id == id).Include(p => p.User).FirstOrDefaultAsync();
        }

        public async Task CreatePost(BlogPost post)
        {
            await _blogDbContext.Posts.AddAsync(post);
            await _blogDbContext.SaveChangesAsync();
        }

        public async Task UpdateBlogPost(BlogPost blogPost)
        {
            _blogDbContext.Entry(blogPost).State = EntityState.Modified;
            await _blogDbContext.SaveChangesAsync();
        }

        public async Task DeleteBlogPost(BlogPost post)
        {
            _blogDbContext.Posts.Remove(post);
            await _blogDbContext.SaveChangesAsync();
        }

    }
}
