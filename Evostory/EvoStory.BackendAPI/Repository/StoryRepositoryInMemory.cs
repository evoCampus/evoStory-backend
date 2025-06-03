using Evostory.Story.Models;
using EvoStory.BackendAPI.Database;

namespace EvoStory.BackendAPI.Repository
{
    public class StoryRepositoryInMemory(ILogger<StoryRepositoryInMemory> logger) : IStoryRepository
    {
        private readonly DatabaseInMemory dbContext = new DatabaseInMemory();
        public Story CreateStory(Story story)
        {
            logger.LogTrace("Create story repository was called.");
            dbContext.Stories.Add(story.Id, story);
            logger.LogInformation($"Story with Id: {story.Id} was created.");
            return story;
        }

        public Story DeleteStory(Guid storyId)
        {
            logger.LogTrace("Delete story repository was called.");
            var result = dbContext.Stories.FirstOrDefault(s => s.Key == storyId).Value;
            if (result != null)
            {
                dbContext.Stories.Remove(storyId);
                logger.LogInformation($"Story with Id: {storyId} was deleted.");
                return result;
            }
            logger.LogWarning($"Story with Id: {storyId} was not found.");
            throw new KeyNotFoundException($"No story with ID {storyId} found.");
        }

        public Story GetStory(Guid storyId)
        {
            logger.LogTrace("Get story repository was called.");
            var result = dbContext.Stories.FirstOrDefault(s => s.Key == storyId).Value;
            if (result != null)
            {
                return result;
            }
            logger.LogWarning($"Story with Id: {storyId} was not found.");
            throw new KeyNotFoundException($"No story with ID {storyId} found.");
        }

        public IEnumerable<Story> GetStories()
        {
            logger.LogTrace("Get stories repository was called.");
            return dbContext.Stories.Values;
        }

        public Story EditStory(Story story)
        {
            logger.LogTrace("Edit story repository was called.");
            if (dbContext.Stories.TryGetValue(story.Id, out var existingStory))
            {
                existingStory.Title = story.Title;
                existingStory.Scenes = story.Scenes;
                existingStory.StartingSceneId = story.StartingSceneId;
                logger.LogDebug($"Story with Id: {story.Id} was edited.");
                return existingStory;
            }
            logger.LogWarning($"Story with Id: {story.Id} was not found.");
            throw new KeyNotFoundException($"No story with ID {story.Id} found.");
        }
    }
}
