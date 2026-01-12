using EvoStory.BackendAPI.DTO;
using EvoStory.Database.Exceptions;
using EvoStory.Database.Models;
using EvoStory.Database.Repository;
using System.Security.Cryptography;
using System.Text;

namespace EvoStory.BackendAPI.Services
{
    public class UserService(IUserRepository userRepository, IDTOConversionService dTOConversion, ILogger<UserService> logger) : IUserService
    {
        public async Task<UserDTO> CreateUser(CreateUserDTO user)
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(user.Password);
                byte[] hashValue = mySHA256.ComputeHash(passwordBytes);

                var newUser = new User()
                {
                    Id = Guid.NewGuid(),
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = Convert.ToBase64String(hashValue)
                };
                logger.LogDebug($"User was created successfully with Id: {newUser.Id}");
                var createdUser = await userRepository.CreateUser(newUser);
                return dTOConversion.ConvertUserToUserDTO(createdUser);
            }
        }

        public async Task<UserDTO> GetUser(Guid userId)
        {
            logger.LogDebug("Get User service was called.");
            var result = await userRepository.GetUser(userId);
            return dTOConversion.ConvertUserToUserDTO(result);
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            logger.LogDebug("Get Users service was called.");
            var result = await userRepository.GetUsers();
            var usersDTO = result.Select(user => dTOConversion.ConvertUserToUserDTO(user));
            return usersDTO;
        }

        public async Task<UserDTO> DeleteUser(Guid userId)
        {
            logger.LogDebug("Delete User service was called.");
            var result = await userRepository.DeleteUser(userId);
            logger.LogDebug($"User with Id: {userId} was deleted.");
            return dTOConversion.ConvertUserToUserDTO(result);
        }

        public async Task<UserDTO> Login(string username, string password)
        {
            logger.LogDebug($"Login service was called for user: {username}");
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(passwordBytes);
                string hashedPassword = Convert.ToBase64String(hash);

                var user = await userRepository.Login(username, hashedPassword);

                if (user == null)
                {
                    throw new RepositoryException("Helytelen felhasználónév vagy jelszó!");
                }

                return dTOConversion.ConvertUserToUserDTO(user);
            }
        }
    }
}
