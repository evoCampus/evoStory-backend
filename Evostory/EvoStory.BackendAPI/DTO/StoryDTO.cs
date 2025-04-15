namespace EvoStory.BackendAPI.DTO
{
    public class StoryDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<SceneDTO> Scenes { get; set; }
        public Guid StartingSceneId { get; set; }
    }
}
