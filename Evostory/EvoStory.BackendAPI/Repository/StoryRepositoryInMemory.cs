using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public class StoryRepositoryInMemory(ILogger<StoryRepositoryInMemory> logger) : IStoryRepository
    {
        private List<Story> _stories = new();
        public Story CreateStory(Story story)
        {
            logger.LogInformation("Create story repository was called.");
            _stories.Add(story);
            return story;
        }

        public Story DeleteStory(Guid storyId)
        {
            logger.LogInformation("Delete story repository was called.");
            var result = _stories.FirstOrDefault(story => story.Id == storyId);
            if (result is null)
            {
                logger.LogWarning($"Story with Id: {storyId} was not found.");
                throw new KeyNotFoundException($"No story with {storyId} found.");
            }

            logger.LogDebug($"Story with Id: {storyId} was deleted.");
            _stories.Remove(result);
            return result;
        }

        public Story GetStory(Guid storyId)
        {
            logger.LogInformation("Get story repository was called.");
            var result = _stories.FirstOrDefault(story => story.Id == storyId);
            if (result is null)
            {
                logger.LogWarning($"Story with Id: {storyId} was not found.");
                throw new KeyNotFoundException($"No story with {storyId} found.");
            }

            return result;
        }

        public IEnumerable<Story> GetStories()
        {
            logger.LogInformation("Get stories repository was called.");
            return _stories;
        }

        public Story EditStory(Story story)
        {
            logger.LogInformation("Edit story repository was called.");
            var result = _stories.FirstOrDefault(s => s.Id == story.Id);
            if (result is null)
            {
                logger.LogWarning($"Story with Id: {story.Id} was not found.");
                throw new KeyNotFoundException($"No story with ID {story.Id} found.");
            }

            result.Title = story.Title;
            result.Scenes = story.Scenes;
            result.StartingSceneId = story.StartingSceneId;
            result.Id = story.Id;

            logger.LogDebug($"Story with Id: {story.Id} was edited.");
            return result;
        }
    }
}
