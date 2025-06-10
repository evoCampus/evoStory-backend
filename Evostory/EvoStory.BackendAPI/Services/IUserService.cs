using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Services
{
    public interface IUserService
    {
        public UserDTO CreateUser(CreateUserDTO user);
        public UserDTO GetUser(Guid userId);
        public IEnumerable<UserDTO> GetUsers();
        public UserDTO DeleteUser(Guid userId);
    }
}
