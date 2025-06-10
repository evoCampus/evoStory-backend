using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Repository;
using System.Security.Cryptography;
using System.Text;

namespace EvoStory.BackendAPI.Services
{
    public class UserService(IUserRepository userRepository, IDTOConversionService dTOConversion, ILogger<UserService> logger) : IUserService
    {
        public UserDTO CreateUser(CreateUserDTO user)
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(user.Password);
                byte[] hashValue = mySHA256.ComputeHash(passwordBytes);

                logger.LogInformation($"Lenght of hashvalue: {hashValue.Length}");
                logger.LogInformation($"Hashvalue: {Convert.ToBase64String(hashValue)}");
                var newUser = new User()
                {
                    Id = Guid.NewGuid(),
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = Convert.ToBase64String(hashValue)
                };
                logger.LogInformation($"User was created successfully with Id: {newUser.Id}");
                userRepository.CreateUser(newUser);
                return dTOConversion.ConvertUserToUserDTO(newUser);
            }
        }

        public UserDTO GetUser(Guid userId)
        {
            logger.LogDebug("Get User service was called.");
            var result = userRepository.GetUser(userId);
            return dTOConversion.ConvertUserToUserDTO(result);
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            logger.LogDebug("Get Users service was called.");
            var result = userRepository.GetUsers();
            var usersDTO = result.Select(user => dTOConversion.ConvertUserToUserDTO(user));
            return usersDTO;
        }

        public UserDTO DeleteUser(Guid userId)
        {
            logger.LogDebug("Delete User service was called.");
            var result = userRepository.DeleteUser(userId);
            logger.LogInformation($"User with Id: {userId} was deleted.");
            return dTOConversion.ConvertUserToUserDTO(result);
        }
    }
}
