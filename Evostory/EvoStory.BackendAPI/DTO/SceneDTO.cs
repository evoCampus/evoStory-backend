namespace EvoStory.BackendAPI.DTO
{
    public class SceneDTO
    {
        public required Guid Id { get; set; }
        public ContentDTO? Content { get; set; }
        public IEnumerable<ChoiceDTO> Choices { get; set; }
    }
}
