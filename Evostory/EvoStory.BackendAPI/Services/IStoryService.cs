using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Services
{
    public interface IStoryService
    {
        public StoryDTO CreateStory(CreateStoryDTO story);
        public StoryDTO GetStory(Guid storyId);
        public IEnumerable<StoryDTO> GetStories();
        public StoryDTO EditStory(EditStoryDTO story);
        public StoryDTO DeleteStory(Guid storyId);
    }
}
