using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public interface IStoryRepository
    {
        public void CreateStory(Story story);
        public Story GetStory(Guid storyId);
        public IEnumerable<Story> GetStories();
        public void EditStory(Story story);
        public void DeleteStory(Guid storyId);
    }
}
