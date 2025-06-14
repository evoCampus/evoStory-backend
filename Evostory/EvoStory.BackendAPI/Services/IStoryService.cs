using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Services
{
    public interface IStoryService
    {
        StoryDTO CreateStory(CreateStoryDTO story);
        StoryDTO GetStory(Guid storyId);
        IEnumerable<StoryDTO> GetStories();
        StoryDTO EditStory(EditStoryDTO story);
        StoryDTO DeleteStory(Guid storyId);
    }
}
