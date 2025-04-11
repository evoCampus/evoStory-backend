
using Evostory.Story.Models;

namespace evoStory.BackendAPI.DTO
{
    public class EditStoryDTO
    {
        public required string Title { get; set; }
        public Guid Id { get; set; }
        public IEnumerable<SceneDTO>? Scenes { get; set; }
        public Guid? StartingSceneId { get; set; }
    }
}
