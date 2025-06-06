using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Services
{
    public interface IStoryService
    {
        void CreateStory(CreateStoryDTO story);
        StoryDTO GetStory(Guid storyId);
        IEnumerable<StoryDTO> GetStories();
        void EditStory(EditStoryDTO story);
        void DeleteStory(Guid storyId);
    }
}
