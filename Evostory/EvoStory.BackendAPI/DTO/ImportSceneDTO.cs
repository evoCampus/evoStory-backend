namespace EvoStory.BackendAPI.DTO
{
    public class ImportSceneDTO
    {
        public int SceneId { get; set; }
        public ImportContentDTO? Content { get; set; }
        public IEnumerable<ImportChoiceDTO>? Choices { get; set; }
    }
}
