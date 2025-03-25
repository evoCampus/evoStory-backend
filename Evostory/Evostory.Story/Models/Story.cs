namespace Evostory.Story.Models
{
    public class Story
    {
        public Guid StoryId { get; set; }
        public string Title { get; set; }
        public IEnumerable<Scene> Scenes { get; set; }
        public Guid StartingSceneId { get; set; }
    }
}
