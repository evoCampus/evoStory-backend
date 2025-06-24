namespace Evostory.Story.Models
{
    public class ImportSceneModel
    {
        public int SceneId { get; set; }
        public ImportContentModel? Content { get; set; }
        public IEnumerable<ImportChoiceModel>? Choices { get; set; }
    }
}
