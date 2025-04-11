namespace Evostory.Story.Models
{
    public class Choice
    {
        public Guid ChoiceId { get; set; }
        public Guid NextSceneId { get; set; }
        public string ChoiceText { get; set; }
    }
}
