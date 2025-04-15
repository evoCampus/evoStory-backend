using Evostory.Story.Models;

namespace evoStory.BackendAPI.DTO
{
    public class CreateSceneDTO
    {
        public Content? Content {  get; set; }
        public Guid StoryId { get; set; }
        public IEnumerable<ChoiceDTO>? Choices { get; set; }
    }
}
