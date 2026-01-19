namespace EvoStory.BackendAPI.DTO
{
    public class CreateChoiceDTO
    {
        public Guid SceneId { get; set; }
        public Guid NextSceneId { get; set; }
        public string? ChoiceText { get; set; }
        public Guid? RequiredItemId { get; set; }
    }
}
