namespace Evostory.Story.Models
{
    public class Choice
    {
        public Guid Id { get; set; }
        public Guid NextSceneId { get; set; }
        public string? ChoiceText { get; set; }
        public Guid SceneId { get; set; }
    }
}
