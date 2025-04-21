using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Services
{
    public interface IStoryService
    {
        public void CreateStory(CreateStoryDTO story);
        public StoryDTO GetStory(Guid storyId);
        public IEnumerable<StoryDTO> GetStories();
        public void EditStory(EditStoryDTO story);
        public void DeleteStory(Guid storyId);
    }
}
