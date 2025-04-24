namespace Evostory.Story.Models
{
    public class Scene
    {
        public Guid Id { get; set; }
        public Content Content { get; set; }
        public IEnumerable<Choice> Choices { get; set; }
    }
}
