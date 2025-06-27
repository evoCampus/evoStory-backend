using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Services
{
    public interface IUserService
    {
        UserDTO CreateUser(CreateUserDTO user);
        UserDTO GetUser(Guid userId);
        IEnumerable<UserDTO> GetUsers();
        UserDTO DeleteUser(Guid userId);
        UserDTO Login(string username, string password);
    }
}
