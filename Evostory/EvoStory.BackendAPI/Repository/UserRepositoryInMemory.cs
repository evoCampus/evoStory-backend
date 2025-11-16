using EvoStory.Database.Exceptions;
using EvoStory.Database.Models;
using EvoStory.Database.Repository;
using Microsoft.EntityFrameworkCore;

namespace EvoStory.BackendAPI.Repository
{
    public class UserRepositoryInMemory(ILogger<UserRepositoryInMemory> logger) : IUserRepository
    {
        private Dictionary<Guid, User> _users = new();
        public Task<User> CreateUser(User user)
        {
            logger.LogTrace("Create user repository was called.");
            if (_users.ContainsKey(user.Id))
            {
                throw new RepositoryException($"Existing user with Id: {user.Id} found.");
            }

            _users.Add(user.Id, user);
            logger.LogInformation("User succesfully created in repository.");
            return Task.FromResult(user);
        }

        public Task<User> GetUser(Guid userId)
        {
            logger.LogTrace("Get user repository was called.");
            var result = _users.FirstOrDefault(user => user.Key == userId);
            if (result.Value is null)
            {
                logger.LogWarning($"User with Id: {userId} was not found.");
                throw new RepositoryException($"No user with Id: {userId} found.");
            }

            return Task.FromResult(result.Value);
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            logger.LogTrace("Get users repository was called.");
            return Task.FromResult(_users.Values as IEnumerable<User>);
        }

        public Task<User> DeleteUser(Guid userId)
        {
            logger.LogTrace("Delete user repository was called.");
            var result = _users.FirstOrDefault(user => user.Key == userId);
            if (result.Value is null)
            {
                logger.LogWarning($"User with Id: {userId} was not found.");
                throw new RepositoryException($"No user with Id: {userId} found.");
            }
            _users.Remove(result.Key);
            logger.LogInformation($"User with Id: {userId} was deleted.");
            return Task.FromResult(result.Value);
        }

        public Task<User> Login(string username, string hashedPassword)
        {
            logger.LogTrace("Login repository was called.");
            var user = _users.Values.FirstOrDefault(u => u.UserName == username && u.Password == hashedPassword);
            if (user == null)
            {
                logger.LogTrace("Invalid username or password provided.");
                throw new RepositoryException("Invalid username or password.");
            }

            return Task.FromResult(user);
        }
    }
}
