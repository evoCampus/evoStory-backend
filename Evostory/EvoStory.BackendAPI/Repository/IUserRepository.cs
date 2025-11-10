using EvoStory.Database.Models;

namespace EvoStory.BackendAPI.Repository
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        User GetUser(Guid userId);
        IEnumerable<User> GetUsers();
        User DeleteUser(Guid sceneId);
        User Login(string username, string hashedPassword);
    }
}
