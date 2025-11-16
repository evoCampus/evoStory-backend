using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Services
{
    public interface IUserService
    {
        Task<UserDTO> CreateUser(CreateUserDTO user);
        Task<UserDTO> GetUser(Guid userId);
        Task<IEnumerable<UserDTO>> GetUsers();
        Task<UserDTO> DeleteUser(Guid userId);
        Task<UserDTO> Login(string username, string password);
    }
}
