using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public class StoryRepositoryInMemory : IStoryRepository
    {
        private List<Story> _stories = new();
        public void CreateStory(Story story)
        {
            _stories.Add(story);
        }

        public void DeleteStory(Guid storyId)
        {
            var result = _stories.FirstOrDefault(story => story.Id == storyId);
            if (result is null)
            {
                throw new KeyNotFoundException($"No story with {storyId} found.");
            }

            _stories.Remove(result);
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

        public void EditStory(Story story)
        {
            var result = _stories.FirstOrDefault(story => story.Id == story.Id);
            if (result is null)
            {
                throw new KeyNotFoundException($"No story with {story.Id} found.");
            }

            _stories.Remove(result);
            _stories.Add(story);
        }
    }
}
