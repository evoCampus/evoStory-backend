namespace EvoStory.BackendAPI.DTO
{
    public class CreateStoryDTO
    {
        public required string Title { get; set; }
        public IEnumerable<CreateSceneDTO> Scenes { get; set; }
        public Guid? StartingSceneId { get; set; }
    }
}
