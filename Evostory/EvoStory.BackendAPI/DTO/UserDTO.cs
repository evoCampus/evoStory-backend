namespace EvoStory.BackendAPI.DTO
{
    public class UserDTO
    {
        public required Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
    }
}
