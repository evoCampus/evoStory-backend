namespace Evostory.Story.Models
{
    public class ChoiceDTO
    {
        public Guid Id { get; set; }
        public Guid NextSceneId { get; set; }
        public string ChoiceText { get; set; }
    }
}
