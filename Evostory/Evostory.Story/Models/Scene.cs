namespace Evostory.Story.Models
{
    public class Scene
    {
        public Guid SceneId { get; set; }
        public Content Content { get; set; }
        public IEnumerable<Choice> Choices { get; set; }
    }
}
