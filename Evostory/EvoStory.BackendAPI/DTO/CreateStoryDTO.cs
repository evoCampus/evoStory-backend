namespace EvoStory.BackendAPI.DTO
{
    public class CreateStoryDTO
    {
        public required string Title { get; set; }
        public IEnumerable<CreateSceneDTO> Scenes { get; set; }
        public int StartingSceneId { get; set; }
        public List<CreateItemDTO> Items { get; set; } = new();
    }
}
