namespace EvoStory.BackendAPI.DTO
{
    public class StoryDTO
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public Guid StartingSceneId { get; set; }
    }
}
