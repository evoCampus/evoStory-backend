using Evostory.Story.Models;

namespace EvoStory.BackendAPI.DTO
{
    public class EditStoryDTO
    {
        public required string Title { get; set; }
        public required Guid Id { get; set; }
        public Guid? StartingSceneId { get; set; }
        public IEnumerable<SceneDTO> Scenes { get; set; }
    }
}