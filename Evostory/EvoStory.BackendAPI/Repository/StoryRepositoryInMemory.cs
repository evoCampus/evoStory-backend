using EvoStory.Database.Models;
using EvoStory.BackendAPI.Database;
using EvoStory.Database.Exceptions;
using EvoStory.Database.Repository;

namespace EvoStory.BackendAPI.Repository
{
    public class StoryRepositoryInMemory(ILogger<StoryRepositoryInMemory> logger, IDatabase dbContext) : IStoryRepository
    {
        public Task<Story> CreateStory(Story story)
        {
            logger.LogTrace("Create story repository was called.");
            if (dbContext.Stories.ContainsKey(story.Id))
            {
                throw new RepositoryException($"Existing story with Id: {story.Id} found.");
            }
            dbContext.Stories.Add(story.Id, story);
            logger.LogInformation($"Story with Id: {story.Id} was created.");
            return Task.FromResult(story);
        }

        public Task<Story> DeleteStory(Guid storyId)
        {
            logger.LogTrace("Delete story repository was called.");
            var result = dbContext.Stories.FirstOrDefault(s => s.Key == storyId).Value;
            if (result is null)
            {
                logger.LogWarning($"Story with Id: {storyId} was not found.");
                throw new RepositoryException($"No story with ID {storyId} found.");
            }
            dbContext.Stories.Remove(storyId);
            logger.LogInformation($"Story with Id: {storyId} was deleted.");
            return Task.FromResult(result);
        }

        public Task<Story> GetStory(Guid storyId)
        {
            logger.LogTrace("Get story repository was called.");
            var result = dbContext.Stories.FirstOrDefault(s => s.Key == storyId).Value;
            if (result != null)
            {
                return Task.FromResult(result);
            }
            logger.LogWarning($"Story with Id: {storyId} was not found.");
            throw new RepositoryException($"No story with ID {storyId} found.");
        }

        public Task<IEnumerable<Story>> GetStories()
        {
            logger.LogTrace("Get stories repository was called.");
            return Task.FromResult(dbContext.Stories.Values as IEnumerable<Story>);
        }

        public Task<Story> EditStory(Story story)
        {
            logger.LogTrace("Edit story repository was called.");
            if (dbContext.Stories.TryGetValue(story.Id, out var existingStory))
            {
                existingStory.Title = story.Title;
                existingStory.Scenes = story.Scenes;
                existingStory.StartingSceneId = story.StartingSceneId;
                logger.LogDebug($"Story with Id: {story.Id} was edited.");
                return Task.FromResult(existingStory);
            }
            logger.LogWarning($"Story with Id: {story.Id} was not found.");
            throw new RepositoryException($"No story with ID {story.Id} found.");
        }
    }
}
