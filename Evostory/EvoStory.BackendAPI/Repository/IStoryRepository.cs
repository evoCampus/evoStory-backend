using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public interface IStoryRepository
    {
        Story CreateStory(Story story);
        Story GetStory(Guid storyId);
        IEnumerable<Story> GetStories();
        Story EditStory(Story story);
        Story DeleteStory(Guid storyId);
    }
}
