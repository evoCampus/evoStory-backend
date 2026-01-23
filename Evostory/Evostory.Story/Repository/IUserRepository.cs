using EvoStory.Database.Models;

namespace EvoStory.Database.Repository
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<User> GetUser(Guid userId);
        Task<IEnumerable<User>> GetUsers();
        Task<User> DeleteUser(Guid sceneId);
        Task<User> Login(string username, string hashedPassword);
    }
}
