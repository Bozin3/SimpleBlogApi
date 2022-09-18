using BlogApi.Data.Entities;

namespace BlogApi.Data.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsername(string username);
        Task RegisterUser(User user);
    }
}
