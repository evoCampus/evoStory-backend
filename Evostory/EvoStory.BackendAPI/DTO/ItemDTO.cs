namespace EvoStory.BackendAPI.DTO
{
    public class ItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsStackable { get; set; }
    }
}