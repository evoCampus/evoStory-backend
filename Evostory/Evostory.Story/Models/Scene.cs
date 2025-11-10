namespace EvoStory.Database.Models
{
    public class Scene
    {
        public required Guid Id { get; set; }
        public Content Content { get; set; }
        public IEnumerable<Choice> Choices { get; set; }
    }
}
