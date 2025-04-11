namespace evoStory.BackendAPI.Models
{
    public class Story
    {
        public required string Title { get; set; }
        public Guid Id { get; set; }
        public List<Scene> Scenes { get; set; }
        public Guid StartingSceneId { get; set; }

    }
}
