using Evostory.Story.Models;

namespace evoStory.BackendAPI.DTO
{
    public class CreateStoryDTO
    {
        public required string Title { get; set; }
        public IEnumerable<SceneDTO>? Scenes { get; set; }
        public Guid? StartingSceneId { get; set; }
    }
}
