namespace evoStory.BackendAPI.Models
{
    public class Scene
    {
        public Guid Id { get; set; }
        public Content Content { get; set; }
        public List<Choice> Choices { get; set; }
    }
}
