namespace Evostory.Story.Models
{
    public class Story
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public IEnumerable<Scene> Scenes { get; set; }
        public Guid StartingSceneId { get; set; }
    }
}
