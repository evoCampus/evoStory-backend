using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public interface IUserRepository
    {
        public User CreateUser(User user);
        public User GetUser(Guid userId);
        public IEnumerable<User> GetUsers();
        public User DeleteUser(Guid sceneId);
    }
}
