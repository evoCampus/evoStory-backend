using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public interface IStoryRepository
    {
        public Story CreateStory(Story story);
        public Story GetStory(Guid storyId);
        public IEnumerable<Story> GetStories();
        public Story EditStory(Story story);
        public Story DeleteStory(Guid storyId);
    }
}
