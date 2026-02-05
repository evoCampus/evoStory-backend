namespace EvoStory.BackendAPI.DTO
{
    public class CreateChoiceDTO
    {
        public int? SceneId { get; set; }
        public int? NextSceneId { get; set; }
        public string? ChoiceText { get; set; }
        public Guid? RequiredItemId { get; set; }
        public string? RewardItemName { get; set; }
    }
}
