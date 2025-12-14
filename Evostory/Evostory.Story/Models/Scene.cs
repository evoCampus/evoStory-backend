namespace EvoStory.Database.Models
{
    public class Scene
    {
        public required Guid Id { get; set; }
        public Content Content { get; set; }
        public ICollection<Choice> Choices { get; set; }
        public Guid StoryId { get; set; }
        public Scene() { 
        Choices = new List<Choice>();
        }
    }
}
