namespace EvoStory.Database.Models
{
    public class ImportChoiceModel
    {
        public int NextSceneId { get; set; }
        public string ChoiceText { get; set; }
        public string? RewardItemName { get; set; }
        public string? RequiredItemName { get; set; }
    }
}
