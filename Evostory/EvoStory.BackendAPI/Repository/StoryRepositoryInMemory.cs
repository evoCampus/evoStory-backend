using Evostory.Story.Models;
using EvoStory.BackendAPI.Database;

namespace EvoStory.BackendAPI.Repository
{
    public class StoryRepositoryInMemory(IDatabase dbContext, ILogger<StoryRepositoryInMemory> logger) : IStoryRepository
    {
        public Story CreateStory(Story story)
        {
            logger.LogTrace("Create story repository was called.");
            dbContext.AddStory(story);
            return story;
        }

        public Story DeleteStory(Guid storyId)
        {
            logger.LogTrace("Delete story repository was called.");
            var result = dbContext.RemoveStory(storyId);
            return result;
        }

        public Story GetStory(Guid storyId)
        {
            logger.LogTrace("Get story repository was called.");
            var result = dbContext.GetStory(storyId);
            return result;
        }

        public IEnumerable<Story> GetStories()
        {
            logger.LogTrace("Get stories repository was called.");
            return dbContext.GetAllStories();
        }

        public Story EditStory(Story story)
        {
            logger.LogTrace("Edit story repository was called.");
            var result = dbContext.UpdateStory(story);
            return result;
        }
    }
}
