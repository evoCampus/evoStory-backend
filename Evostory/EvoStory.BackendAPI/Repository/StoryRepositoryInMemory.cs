using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public class StoryRepositoryInMemory : IStoryRepository
    {
        private List<Story> _stories = new();
        public Story CreateStory(Story story)
        {
            _stories.Add(story);
            return story;
        }

        public Story DeleteStory(Guid storyId)
        {
            var result = _stories.FirstOrDefault(story => story.Id == storyId);
            if (result is null)
            {
                throw new KeyNotFoundException($"No story with {storyId} found.");
            }

            _stories.Remove(result);
            return result;
        }

        public Story GetStory(Guid storyId)
        {
            var result = _stories.FirstOrDefault(story => story.Id == storyId);
            if (result is null)
            {
                throw new KeyNotFoundException($"No story with {storyId} found.");
            }

            return result;
        }

        public IEnumerable<Story> GetStories()
        {
            return _stories;
        }

        public Story EditStory(Story story)
        {
            var result = _stories.FirstOrDefault(s => s.Id == story.Id);
            if (result is null)
            {
                throw new KeyNotFoundException($"No story with ID {story.Id} found.");
            }

            result.Title = story.Title;
            result.Scenes = story.Scenes;
            result.StartingSceneId = story.StartingSceneId;
            result.Id = story.Id;

            return result;
        }
    }
}
