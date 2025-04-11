using evoStory.BackendAPI.Models;

namespace evoStory.BackendAPI.DTO
{
    public class CreateSceneDTO
    {
        public Content? Content {  get; set; }
        public Guid StoryId { get; set; }
        public IEnumerable<Choice>?  Choices { get; set; }
    }
}
