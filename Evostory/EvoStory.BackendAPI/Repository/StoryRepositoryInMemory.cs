using Evostory.Story.Models;
using EvoStory.BackendAPI.Database;

namespace EvoStory.BackendAPI.Repository
{
    public class StoryRepositoryInMemory(IDatabase dbContext) : IStoryRepository
    {
        public Story CreateStory(Story story)
        {
            dbContext.AddStory(story);
            return story;
        }

        public Story DeleteStory(Guid storyId)
        {
            var result = dbContext.RemoveStory(storyId);
            return result;
        }

        public Story GetStory(Guid storyId)
        {
            var result = dbContext.GetStory(storyId);
            return result;
        }

        public IEnumerable<Story> GetStories()
        {
            return dbContext.GetAllStories();
        }

        public Story EditStory(Story story)
        {
            var result = dbContext.UpdateStory(story);
            return result;
        }
    }
}
