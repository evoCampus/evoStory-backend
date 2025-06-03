using Evostory.Story.Models;
using EvoStory.BackendAPI.Exceptions;

namespace EvoStory.BackendAPI.Repository
{
    public class StoryRepositoryInMemory(ILogger<StoryRepositoryInMemory> logger) : IStoryRepository
    {
        private Dictionary<Guid, Story> _stories = new();
        public Story CreateStory(Story story)
        {
            logger.LogTrace("Create story repository was called.");
            if (_stories.ContainsKey(story.Id))
            {
                throw new RepositoryException($"Existing story with {story.Id} found.");
            }
            
            _stories.Add(story.Id, story);
            return story;
        }

        public Story DeleteStory(Guid storyId)
        {
            logger.LogTrace("Delete story repository was called.");
            var result = _stories.FirstOrDefault(story => story.Key == storyId);
            if (result.Value is null)
            {
                logger.LogWarning($"Story with Id: {storyId} was not found.");
                throw new RepositoryException($"No story with {storyId} found.");
            }
            
            logger.LogDebug($"Story with Id: {storyId} was deleted.");
            _stories.Remove(result.Key);
            return result.Value;
        }

        public Story GetStory(Guid storyId)
        {
            logger.LogTrace("Get story repository was called.");
            var result = _stories.FirstOrDefault(story => story.Key == storyId);
            if (result.Value is null)
            {
                logger.LogWarning($"Story with Id: {storyId} was not found.");
                throw new RepositoryException($"No story with {storyId} found.");
            }

            return result.Value;
        }

        public IEnumerable<Story> GetStories()
        {
            logger.LogTrace("Get stories repository was called.");
            return _stories.Values;
        }

        public Story EditStory(Story story)
        {
            logger.LogTrace("Edit story repository was called.");
            var result = _stories.FirstOrDefault(s => s.Key == story.Id);
            if (result.Value is null)
            {
                logger.LogWarning($"Story with Id: {story.Id} was not found.");
                throw new RepositoryException($"No story with ID {story.Id} found.");
            }

            _stories[result.Key].Title = story.Title;
            _stories[result.Key].Scenes = story.Scenes;
            _stories[result.Key].StartingSceneId = story.StartingSceneId;
            _stories[result.Key].Id = story.Id;

            logger.LogDebug($"Story with Id: {story.Id} was edited.");
            return result.Value;
        }
    }
}
