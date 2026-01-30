namespace EvoStory.Database.Models
{
    public class ImportStoryModel
    {
        public string Title { get; set; }
        public IEnumerable<ImportSceneModel> Scenes { get; set; }
        public int StartingSceneId { get; set; }
        public List<ImportItemModel> Items { get; set; }

    }
}
