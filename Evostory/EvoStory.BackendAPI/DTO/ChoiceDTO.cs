namespace EvoStory.BackendAPI.DTO
{
    public class ChoiceDTO
    {
        public Guid Id { get; set; }
        public Guid NextSceneId { get; set; }
        public string ChoiceText { get; set; }
    }
}