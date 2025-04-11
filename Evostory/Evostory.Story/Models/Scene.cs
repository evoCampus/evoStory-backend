namespace Evostory.Story.Models
{
    public class SceneDTO
    {
        public Guid Id { get; set; }
        public Content Content { get; set; }
        public IEnumerable<ChoiceDTO> Choices { get; set; }
    }
}
