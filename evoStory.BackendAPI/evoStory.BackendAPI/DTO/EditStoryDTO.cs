using evoStory.BackendAPI.Models;

namespace evoStory.BackendAPI.DTO
{
    public class EditStoryDTO
    {
        public required string Title { get; set; }
        public Guid Id { get; set; }
        public IEnumerable<Scene>? Scenes { get; set; }
        public Guid? StartingSceneId { get; set; }

    }
}
