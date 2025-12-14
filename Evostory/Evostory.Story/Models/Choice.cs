namespace EvoStory.Database.Models
{
    public class Choice
    {
        public required Guid Id { get; set; }
        public Guid NextSceneId { get; set; }
        public string? ChoiceText { get; set; }
        public Guid SceneId { get; set; }
        public Scene Scene { get; set; }
    }
}
