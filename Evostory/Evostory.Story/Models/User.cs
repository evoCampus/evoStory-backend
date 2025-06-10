namespace Evostory.Story.Models
{
    public class User
    {
        public required Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
