using Evostory.Story.Models;

namespace EvoStory.BackendAPI.DTO
{
    public class CreateSceneDTO
    {
        public Content? Content { get; set; }
        public Guid StoryId { get; set; }
        public IEnumerable<CreateChoiceDTO>? Choices { get; set; }
    }
}
